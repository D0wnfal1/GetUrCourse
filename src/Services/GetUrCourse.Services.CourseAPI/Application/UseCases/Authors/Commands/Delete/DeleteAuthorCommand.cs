using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Authors.Commands.Delete;

public record DeleteAuthorCommand(Guid Id) : ICommand;
