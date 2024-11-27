using System.Linq.Expressions;
using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Enums;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Queries.GetCourses;

public class GetCoursesQueryHandler(CourseDbContext context) : IQueryHandler<GetCoursesQuery,PagedList<CourseShortResponse>>
{
    public async Task<Result<PagedList<CourseShortResponse>>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var query = context.Courses.AsQueryable();
        
        query = FilterQuery(request, query);

        var projection = query.Select(c => new CourseShortResponse(
            c.Id,
            c.ImageUrl,
            c.Title,
            c.FullDescription,
            c.Rating.Value,
            c.Price,
            c.TotalDuration.TotalHours,
            Enum.GetName(typeof(Level), c.Level) ?? "unknown",
            c.Authors.Select(a => a.FullName).ToList()));
        
        var result = await PagedList<CourseShortResponse>
            .CreateAsync(projection, request.PageNumber, request.PageSize);
        
        if (result.IsFailure)
            return Result.Failure<PagedList<CourseShortResponse>>(result.Error);
       
        return Result.Success(result.Value!);
    }

    private static IQueryable<Course> FilterQuery(GetCoursesQuery request, IQueryable<Course> query)
    {
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            query = query.Where(c => c.Title.Contains(request.SearchTerm));

        if (request.Rating.HasValue)
            query = query.Where(c => c.Rating.Value >= request.Rating.Value);

        if (request.IsFree.HasValue)
            query = query.Where(c => c.Price == 0 == request.IsFree.Value);

        if (request.Category != Guid.Empty)
            query = query.Where(c => c.Category.Id == request.Category);

        if (request.SubCategory != null && request.SubCategory.Any())
            query = query
                .Include(c => c.Category)
                .Where(c => request.SubCategory.Contains(c.Category.Id));

        if (request.Levels is not null && request.Levels.Count != 0 )
            query = query.Where(c => request.Levels.Contains(c.Level));
        
        if (request.Languages is not null && request.Languages.Count != 0)
            query = query.Where(c => request.Languages.Contains(c.Language));

        if (!string.IsNullOrWhiteSpace(request.SortColumn))
        {
            query = request.SortOrder?.ToLower() switch
            {
                "desc" => query.OrderByDescending(GetSortExpression(request.SortColumn)),
                _ => query.OrderBy(GetSortExpression(request.SortColumn)),
            };
        }

        return query;
    }

    private static Expression<Func<Course, object>> GetSortExpression(string sortColumn)
    {
        return sortColumn.ToLower() switch
        {
            "title" => c => c.Title,
            "rating" => c => c.Rating.Value,
            "price" => c => c.Price,
            "category" => c => c.Category.Title,
            "level" => c => c.Level,
            "views" => c => c.CountOfViews,
            _ => c => c.CountOfViews,
        };
    }
    
    
}