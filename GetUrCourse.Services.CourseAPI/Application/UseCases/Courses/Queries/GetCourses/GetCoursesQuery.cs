using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Enums;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Queries.GetCourses;

public record GetCoursesQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    double? Rating,
    bool? IsFree,
    Guid Category,
    List<Guid>? SubCategory,
    List<Language>? Languages,
    List<Level>? Levels,
    int PageNumber,
    int PageSize
) : IQuery<PagedList<CourseShortResponse>>;