using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Queries.GetById;

public class GetSubscriptionByIdQueryHandler(CourseDbContext context) : IQueryHandler<GetSubscriptionByIdQuery, SubscriptionResponse>
{
    public async Task<Result<SubscriptionResponse>> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var subscription = await context.Subscriptions
            .AsNoTracking()
            .Where(s => s.Id == request.Id)
            .Select(s => new SubscriptionResponse(
                s.Id,
                s.Title,
                s.Description,
                s.Price,
                s.DiscountPrice))
            .FirstOrDefaultAsync(cancellationToken);

        if (subscription is null)
            return Result.Failure<SubscriptionResponse>(new Error("get_subscription", "Subscription not found."));

        return Result.Success(subscription);
    }
}