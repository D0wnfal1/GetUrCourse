using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Contracts;

namespace GetUrCourse.Services.CourseAPI.Application.Courses.Queries.GetCourses;

public record GetCoursesQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    double? Rating,
    bool? IsFree,
    Guid Category,
    List<Guid>? SubCategory,
    int? Level,
    int PageNumber,
    int PageSize
) : IQuery<PagedList<CoursesResponse>>;