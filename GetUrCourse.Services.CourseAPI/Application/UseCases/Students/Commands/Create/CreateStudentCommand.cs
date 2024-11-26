using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.Create;

public record CreateStudentCommand(
    Guid Id, 
    string FullName, 
    string? ImageUrl) : ICommand;