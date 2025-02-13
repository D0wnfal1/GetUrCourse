using System.Linq.Expressions;
using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Reviews.Queries.GetByAutorId;

public class GetReviewsByAuthorIdQueryHandler(UserDbContext context) : IQueryHandler<GetReviewsByAuthorIdQuery, PagedList<ReviewResponse>>
{
    public async Task<Result<PagedList<ReviewResponse>>> Handle(GetReviewsByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Review> reviewsQuery = context.Reviews
            .AsNoTracking()
            .Where(c => c.AuthorId == request.AuthorId)
            .Include(r => r.Student)
            .ThenInclude(s => s.User)
            .AsSplitQuery();
        
        reviewsQuery = request.SortAscending?.ToLower() == "desc" ? 
            reviewsQuery.OrderByDescending(GetSortProperty(request)) : 
            reviewsQuery.OrderBy(GetSortProperty(request));

        var reviewsResponseQuery = reviewsQuery
            .Select(r => new ReviewResponse(
                r.Id,
                r.StudentId,
                r.Student.User.ImageUrl ?? string.Empty,
                r.Student.User.Name.ToString(),
                r.Text,
                r.Rating,
                r.CreatedAt));
        
        var result = await PagedList<ReviewResponse>
            .CreateAsync(reviewsResponseQuery, request.PageNumber, request.PageSize);

        if (result.IsFailure)
        {
            return Result.Failure<PagedList<ReviewResponse>>(
                new Error("get_reviews",result.Error));
        }
        return Result.Success(result.Value!);
    }
    
    private static Expression<Func<Review, object>> GetSortProperty(GetReviewsByAuthorIdQuery request) => 
        request.SortColumn?.ToLower() switch
        {
            "date" => r => r.CreatedAt,
            "student" => r => r.StudentId,
            _ => r => r.CreatedAt
        };
}