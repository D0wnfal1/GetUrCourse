using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.Create;

public class CreateStudentCommandHandler(CourseDbContext context) : ICommandHandler<CreateStudentCommand>
{
    public async Task<Result> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = Student.Create(request.Id, request.FullName, request.ImageUrl);

        if (student.IsFailure)
        {
            return Result.Failure(student.Error);
        }
        
        await context.Students.AddAsync(student.Value, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}