using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Reviews.Queries.GetById;

public class GetReviewByIdQueryHandler(UserDbContext context) : IQueryHandler<GetReviewByIdQuery, ReviewResponse>
{
    public async Task<Result<ReviewResponse>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await context.Reviews
            .AsNoTracking()
            .Where(r => r.Id == request.Id)
            .Include(r => r.Student)
            .ThenInclude(s => s.User)
            .AsSplitQuery()
            .Select(rvw => new ReviewResponse(
                rvw.Id,
                rvw.StudentId,
                rvw.Student.User.ImageUrl ?? string.Empty,
                rvw.Student.User.Name.ToString(),
                rvw.Text,
                rvw.Rating,
                rvw.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);

        if (review is null)
        {
            return Result.Failure<ReviewResponse>(new Error(
                "get_review",
                "Problem with getting  review from database"));
        }

        return Result.Success(review);
    }
}