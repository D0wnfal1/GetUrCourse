using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Commands.Delete;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.Delete;

public class DeleteCourseCommandHandler(CourseDbContext context) : ICommandHandler<DeleteModuleCommand>
{
    public async Task<Result> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.CourseModules
                .Where(a => a.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new Error("delete_module", "Problem with deleting" + e.Message));
        }

        return Result.Success();
    }
}