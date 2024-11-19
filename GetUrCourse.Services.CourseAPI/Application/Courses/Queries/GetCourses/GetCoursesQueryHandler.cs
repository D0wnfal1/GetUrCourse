using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Contracts;
using GetUrCourse.Services.CourseAPI.Core.Enums;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.Courses.Queries.GetCourses;

public class GetCoursesQueryHandler(CourseDbContext context) : IQueryHandler<GetCoursesQuery,PagedList<CoursesResponse>>
{
    public async Task<Result<PagedList<CoursesResponse>>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var query = context.Courses.AsQueryable();
        
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

        if (request.Level.HasValue && Enum.IsDefined(typeof(Levels), request.Level.Value))
            query = query.Where(c => c.Level == request.Level);


        if (!string.IsNullOrWhiteSpace(request.SortColumn))
        {
            query = request.SortOrder?.ToLower() switch
            {
                "desc" => query.OrderByDescending(GetSortExpression(request.SortColumn)),
                _ => query.OrderBy(GetSortExpression(request.SortColumn)),
            };
        }
        
        var projection = query.Select(c => new CoursesResponse(
            c.Id,
            c.ImageUrl,
            c.Title,
            c.FullDescription,
            c.Rating.Value,
            c.Price,
            c.TotalDuration,
            Enum.GetName(typeof(Levels), c.Level) ?? "unknown",
            c.Authors.Select(a => a.FullName).ToList()));
        
        var result = await PagedList<CoursesResponse>
            .CreateAsync(projection, request.PageNumber, request.PageSize);
        
        if (result.IsFailure)
            return Result.Failure<PagedList<CoursesResponse>>(result.Error);
       
        return Result.Success(result.Value);
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
            _ => c => c.Title,
        };
    }
}