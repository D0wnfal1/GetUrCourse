using System.Diagnostics;
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
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure(new Error("create_comment", "Problem with creating comment"));
            }

            await context.CourseComments.AddAsync(comment.Value!, cancellationToken);

            var course = await context.Courses
                .Where(c => c.Id == request.CourseId)
                .FirstOrDefaultAsync(cancellationToken);

            if (course is null)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure(new Error("create_comment", "Problem with updating course rating"));
            }

            course.Rating.Add(request.Rating);

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("create_comment", "Problem with creating comment"));
        }

        return Result.Success();
    }
}