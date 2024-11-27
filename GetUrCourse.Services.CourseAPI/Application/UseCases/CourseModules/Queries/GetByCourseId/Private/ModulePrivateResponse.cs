namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Queries.GetByCourseId.Private;

public record ModulePrivateResponse(
    Guid Id,
    string Title,
    string Description,
    string VideoUrl,
    TimeSpan Duration,
    string PdfUrl);
