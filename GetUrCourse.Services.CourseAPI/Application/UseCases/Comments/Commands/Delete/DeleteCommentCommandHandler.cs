using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Comments.Commands.Delete;

public class DeleteCommentCommandHandler(CourseDbContext context) : ICommandHandler<DeleteCommentCommand>
{
    public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.CourseComments
                .Where(a => a.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("delete_comment", "Problem with deleting" + e.Message));
        }

        return Result.Success();
    }
}