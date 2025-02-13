using System.Runtime.Intrinsics.X86;
using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Queries.GetAll;

public class GetAllSubscriptionsQueryHandler(CourseDbContext context) : IQueryHandler<GetAllSubscriptionsQuery, IEnumerable<SubscriptionResponse>>
{
    public async Task<Result<IEnumerable<SubscriptionResponse>>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await context.Subscriptions
            .AsNoTracking()
            .OrderBy(x => x.Title)
            .Select(s => new SubscriptionResponse(
                s.Id, 
                s.Title, 
                s.Description, 
                s.Price, 
                s.DiscountPrice))
            .ToListAsync(cancellationToken);
        
        if (subscriptions.Count == 0)
        {
            return Result.Failure<IEnumerable<SubscriptionResponse>>(new Error("get_all_subscriptions", "No subscriptions found"));
        }
        return Result.Success<IEnumerable<SubscriptionResponse>>(subscriptions);
    }
}