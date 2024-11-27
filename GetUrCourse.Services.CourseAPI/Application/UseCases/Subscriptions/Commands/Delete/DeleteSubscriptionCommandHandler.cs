using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.Delete;

public class DeleteSubscriptionCommandHandler(CourseDbContext context) : ICommandHandler<DeleteSubscriptionCommand>
{
    public async Task<Result> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.Subscriptions
                .Where(a => a.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("delete_subscription", "Problem with subscription deleting " + e.Message));
        }
    }
}