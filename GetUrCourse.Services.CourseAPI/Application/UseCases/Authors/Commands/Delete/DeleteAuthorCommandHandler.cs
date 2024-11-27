using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Authors.Commands.Delete;

public class DeleteAuthorCommandHandler(CourseDbContext context) : ICommandHandler<DeleteAuthorCommand>
{
    public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.Authors
                .Where(a => a.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("delete_author", "Problem with deleting " + e.Message));
        }
    }
}