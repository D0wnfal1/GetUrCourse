using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.Delete;

public record DeleteStudentCommand(Guid Id) : ICommand;
