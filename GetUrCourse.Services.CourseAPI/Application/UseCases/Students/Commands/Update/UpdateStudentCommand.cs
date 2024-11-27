using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.Update;

public record UpdateStudentCommand(
    Guid Id, 
    string? FullName, 
    string? ImageUrl) : ICommand;
