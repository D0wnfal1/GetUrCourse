using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.Update;

public class UpdateSubscriptionCommandHandler(CourseDbContext context) : ICommandHandler<UpdateSubscriptionCommand>
{
    public async Task<Result> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await context.Subscriptions
            .Where(a => a.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (subscription == null)
        {
            return Result.Failure(new Error("update_subscription", "Subscription not found"));
        }

        subscription.Update(request.Title, request.Description, request.Price, request.DiscountPrice);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}