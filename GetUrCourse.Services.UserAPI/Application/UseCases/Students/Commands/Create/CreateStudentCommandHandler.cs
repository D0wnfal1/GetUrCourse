using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Commands.Create;

public class CreateStudentCommandHandler(UserDbContext context) : ICommandHandler<CreateStudentCommand>
{
    public async Task<Result> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = Student.Create(request.UserId);
        if (student.IsFailure)
        {
            return Result.Failure(student.Error);
        }
        
        await context.Students.AddAsync(student.Value!, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
        
    }
}