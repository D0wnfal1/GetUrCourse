using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Enums;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Queries.GetCoursesBySubscription;

public class GetCoursesBySubscriptionQueryHandler(CourseDbContext context) : IQueryHandler<GetCoursesBySubscriptionQuery, PagedList<CourseShortResponse>>
{
    public async Task<Result<PagedList<CourseShortResponse>>> Handle(GetCoursesBySubscriptionQuery request, CancellationToken cancellationToken)
    {
        var coursesQuery = context.Courses
            .AsNoTracking()
            .Where(c => c.Subscriptions.Any(s => s.Id == request.SubscriptionId))
            .OrderBy(c => c.CreatedAt)
            .Select(c => new CourseShortResponse(
                c.Id,
                c.ImageUrl,
                c.Title,
                c.FullDescription,
                c.Rating.Value,
                c.Price,
                c.TotalDuration.TotalHours,
                Enum.GetName(typeof(Level), c.Level) ?? "unknown",
                c.Authors.Select(a => a.FullName)));
        
        var result = await PagedList<CourseShortResponse>
            .CreateAsync(coursesQuery, request.PageNumber, request.PageSize);

        if (result.IsFailure)
            return Result.Failure<PagedList<CourseShortResponse>> (result.Error);
        
        return Result.Success(result.Value!);
    }
}