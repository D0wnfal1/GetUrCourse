using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

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
                [request.Id] ,cancellationToken: cancellationToken);
        
            if (comment is null)
                return ValidationResult<UpdateCommentCommand>.WithError(
                    new Error("comment_not_found", "Comment not found"));
        
            comment.Update(
                request.Text,
                request.Rating);

            var course = await context.Courses
                .FindAsync(
                [comment.CourseId] ,cancellationToken: cancellationToken);

            if (course is null)
            {
                await transaction.RollbackAsync(cancellationToken);
                return ValidationResult<UpdateCommentCommand>.WithError(
                    new Error("course_not_found", "Course not found"));
            }
               

            course.Rating.Update(comment.Rating, request.Rating);

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