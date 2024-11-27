using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Commands.Delete;

public class DeleteStudentCommandHandler(UserDbContext context) : ICommandHandler<DeleteStudentCommand>
{
    public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.Students
                .Where(u => u.UserId == request.Id)
                .ExecuteDeleteAsync(cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            
            return Result.Failure(new Error("delete_student",e.Message));
        }
    }
}