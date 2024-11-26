using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Commands.Delete;

public record DeleteStudentCommand(Guid Id) : ICommand;
