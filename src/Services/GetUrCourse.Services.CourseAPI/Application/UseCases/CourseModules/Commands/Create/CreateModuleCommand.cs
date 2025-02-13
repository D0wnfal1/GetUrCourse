using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Commands.Create;

public record CreateModuleCommand(
    string Title,
    string Description,
    string VideoUrl,
    TimeSpan Duration,
    string PdfUrl,
    Guid CourseId) : ICommand;
