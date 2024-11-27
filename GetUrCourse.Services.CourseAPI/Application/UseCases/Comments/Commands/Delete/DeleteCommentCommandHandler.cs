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
                .Where(c => c.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);
            
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
            return Result.Failure(new Error("delete_comment", "Problem with deleting" + e.Message));
        }
    }
}