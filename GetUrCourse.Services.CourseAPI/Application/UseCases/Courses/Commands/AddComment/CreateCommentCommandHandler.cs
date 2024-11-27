using System.Diagnostics;
using System.Dynamic;
using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.AddComment;

public class CreateCommentCommandHandler(CourseDbContext context) : ICommandHandler<CreateCommentCommand>
{
    public async Task<Result> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var comment = CourseComment.Create(
                request.CourseId,
                request.StudentId,
                request.Text,
                request.Rating);

            if (comment.IsFailure)
            {
                return Result.Failure(new Error("create_comment", "Problem with creating comment"));
            }
            
            await context.CourseComments.AddAsync(comment.Value!, cancellationToken);
            
            await context.SaveChangesAsync(cancellationToken);

            await context.Courses
                    .Where(x => x.Id == request.CourseId)
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
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("create_comment", "Problem with creating comment" + e.Message));
        }

        return Result.Success();
    }
}