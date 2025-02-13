using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.Update;

public class UpdateStudentCommandHandler(CourseDbContext context): ICommandHandler<UpdateStudentCommand>
{
    public async Task<Result> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await context.Students
            .Where(c => c.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (student is null)
        {
            return Result.Failure(
                new Error(
                    "update_student", 
                    "Student not found"));
        }
        
        student.Update(
            request.FullName,
            request.ImageUrl);
        
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}