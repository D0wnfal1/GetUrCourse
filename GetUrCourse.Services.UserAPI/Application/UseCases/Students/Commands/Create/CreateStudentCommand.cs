using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Commands.Create;

public record CreateStudentCommand(Guid UserId) : ICommand
{
    
}