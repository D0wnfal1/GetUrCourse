using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.AddComment;

public record CreateCommentCommand(
    Guid CourseId,
    Guid StudentId,
    string Text,
    int Rating) : ICommand;