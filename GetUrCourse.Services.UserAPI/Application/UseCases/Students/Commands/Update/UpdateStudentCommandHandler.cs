using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Commands.Update;

public class UpdateStudentCommandHandler(UserDbContext context) : ICommandHandler<UpdateStudentCommand>
{
    public async Task<Result> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await context.Students.FindAsync(request.Id);
        if (student is null)
        {
            return Result.Failure(new Error("update_student", "student not found"));
        }
        
        student.Update(
            request.CoursesInProgress,
            request.CoursesCompleted);
        
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}