using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.UpdateSubscriptionStatus;

public class UpdateSubscriptionStatusCommandHandler(CourseDbContext context) : ICommandHandler<UpdateSubscriptionStatusCommand>
{
    public async Task<Result> Handle(UpdateSubscriptionStatusCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.StudentSubscriptions
                .ExecuteUpdateAsync(setter => setter
                        .SetProperty(c => c.Status, c => request.Status),
                    cancellationToken: cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("update_subscription_status", e.Message));
        }
    }
}