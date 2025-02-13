using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Comments.Commands.Delete;

public record DeleteCommentCommand(Guid Id) : ICommand;
