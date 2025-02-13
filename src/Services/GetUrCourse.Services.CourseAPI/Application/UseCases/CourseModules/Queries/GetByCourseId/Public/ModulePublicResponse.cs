namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Queries.GetByCourseId.Public;

public record ModulePublicResponse(
    Guid Id,
    string Title,
    string Description);
