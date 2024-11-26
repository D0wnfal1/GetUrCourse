using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Comments.Commands.Update;

public record UpdateCommentCommand(
    Guid Id,
    string Text,
    int Rating) : ICommand;
