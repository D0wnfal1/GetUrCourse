using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Comments.Queries.GetByCourseId;

public record GetCommentsByCourseIdQuery(
    Guid Id,
    int PageNumber,
    int PageSize) : IQuery<PagedList<CourseCommentResponse>>;
