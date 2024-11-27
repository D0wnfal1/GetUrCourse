using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Delete;

public class DeleteUserCommandHandler(UserDbContext context) : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.Users
                .Where(u => u.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("delete_user",e.Message));;
        }
        
        return Result.Success();
    }
}