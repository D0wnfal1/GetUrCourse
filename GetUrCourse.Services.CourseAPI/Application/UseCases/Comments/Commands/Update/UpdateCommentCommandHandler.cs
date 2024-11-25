using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Comments.Commands.Update;

public class UpdateCommentCommandHandler(CourseDbContext context) : ICommandHandler<UpdateCommentCommand>
{
    public async Task<Result> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var comment = await context.CourseComments.FindAsync(
                [request.Id] ,cancellationToken: cancellationToken);
        
            if (comment is null)
                return ValidationResult<UpdateCommentCommand>.WithError(
                    new Error("comment_not_found", "Comment not found"));
        
            comment.Update(
                request.Text,
                request.Rating);

            await context.Courses
                .ExecuteUpdateAsync(setter =>
                        setter.SetProperty(x => x.Rating,
                                x => x.Rating.Update(comment.Rating, request.Rating)),
                    cancellationToken: cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("update_comment", "Problem with updating" + e.Message));
        }
        
        
        return Result.Success();
    }
}