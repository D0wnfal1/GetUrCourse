using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Commands.Delete;

public class DeleteManagerCommandHandler(UserDbContext context) : ICommandHandler<DeleteManagerCommand>
{
    public async Task<Result> Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.Managers
                .Where(u => u.UserId == request.Id)
                .ExecuteDeleteAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            
            return Result.Failure(new Error("delete_manager",e.Message));
        }
        
        return Result.Success();
    }
}