namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Queries;

public record ModuleResponse(
    Guid Id,
    string Title,
    string Description,
    string VideoUrl,
    TimeSpan Duration,
    string PdfUrl);
