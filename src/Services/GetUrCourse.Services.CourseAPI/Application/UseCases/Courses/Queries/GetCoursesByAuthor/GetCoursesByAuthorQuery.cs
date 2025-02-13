using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Queries.GetCoursesByAuthor;

public record GetCoursesByAuthorQuery(
    Guid AuthorId,
    int PageNumber,
    int PageSize) : IQuery<PagedList<CourseShortResponse>>;
