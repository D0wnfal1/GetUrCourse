using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Commands.Update;

public record UpdateStudentCommand(
    Guid Id,
    int? CoursesInProgress, 
    int? CoursesCompleted) : ICommand;