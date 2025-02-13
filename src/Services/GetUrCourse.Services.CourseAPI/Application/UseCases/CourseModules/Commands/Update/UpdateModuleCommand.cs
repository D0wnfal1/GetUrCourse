
using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Commands.Update;

public record UpdateModuleCommand(
    Guid Id,
    string? Title, 
    string? VideoUrl,
    TimeSpan? Duration,
    string? Description, 
    string? PdfUrl): ICommand;

