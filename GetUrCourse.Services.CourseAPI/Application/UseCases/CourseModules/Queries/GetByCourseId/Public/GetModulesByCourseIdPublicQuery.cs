using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Queries.GetByCourseId.Public;

public record GetModulesByCourseIdPublicQuery(
    Guid CourseId,
    int PageNumber,
    int PageSize) : IQuery<PagedList<ModulePublicResponse>>;
