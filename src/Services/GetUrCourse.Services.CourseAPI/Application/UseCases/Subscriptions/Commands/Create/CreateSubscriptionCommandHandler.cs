using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.Create;

public class CreateSubscriptionCommandHandler(CourseDbContext context) : ICommandHandler<CreateSubscriptionCommand>
{
    public async Task<Result> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = Subscription.Create(
            request.Title,
            request.Description,
            request.Price,
            request.DiscountPrise);

        if (subscription.IsFailure)
        {
            return Result.Failure(subscription.Error);
        }
        
        await context.Subscriptions.AddAsync(subscription.Value!, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}