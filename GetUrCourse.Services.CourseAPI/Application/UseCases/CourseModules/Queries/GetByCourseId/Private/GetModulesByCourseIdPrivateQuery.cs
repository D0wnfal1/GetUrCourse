using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Queries.GetByCourseId.Private;

public record GetModulesByCourseIdPrivateQuery(
    Guid CourseId,
    int PageNumber,
    int PageSize) : IQuery<PagedList<ModulePrivateResponse>>;
