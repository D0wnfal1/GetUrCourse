using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Queries.GetByCourseId;

public record GetModulesByCourseIdQuery(
    Guid CourseId,
    int PageNumber,
    int PageSize) : IQuery<PagedList<ModuleResponse>>;
