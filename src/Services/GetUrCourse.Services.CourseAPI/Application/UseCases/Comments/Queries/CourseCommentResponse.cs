namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Comments.Queries;

public record CourseCommentResponse(
    Guid Id,
    string Text,
    int Rating,
    Guid CourseId,
    Guid UserId,
    string UserName,
    DateTime CreatedAt
);
