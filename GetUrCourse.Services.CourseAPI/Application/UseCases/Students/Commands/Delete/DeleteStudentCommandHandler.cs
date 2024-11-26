using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.Delete;

public class DeleteStudentCommandHandler(CourseDbContext context) : ICommandHandler<DeleteStudentCommand>
{
    public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.Students
                .Where(a => a.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("delete_student", "Problem with deleting" + e.Message));
        }

        return Result.Success();
    }
}