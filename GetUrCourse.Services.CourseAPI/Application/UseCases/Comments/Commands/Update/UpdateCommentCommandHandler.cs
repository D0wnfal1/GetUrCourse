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
            var comment = await context.CourseComments
                .FindAsync(
                [request.Id], cancellationToken: cancellationToken);

            if (comment is null)
                return ValidationResult<UpdateCommentCommand>.WithError(
                    new Error("comment_not_found", "Comment not found"));
            
            comment.Update(
                request.Text,
                request.Rating);
            
            await context.SaveChangesAsync(cancellationToken);
            
            await context.Courses
                .Select(c => new
                {
                    Course = c,
                    NewRating = c.Comments.Average(cc => cc.Rating),
                    NewCount = c.Comments.Count
                })
                .ExecuteUpdateAsync(setter => setter
                        .SetProperty(c => c.Course.Rating.Value, c => c.NewRating)
                        .SetProperty(c => c.Course.Rating.Count, c => c.NewCount),
                    
                    cancellationToken: cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("update_comment", "Problem with updating" + e.Message));
        }
    }
}