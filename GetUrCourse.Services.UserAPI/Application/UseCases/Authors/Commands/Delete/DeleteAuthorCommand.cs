using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Authors.Commands.Delete;

public record DeleteAuthorCommand(Guid Id) : ICommand;
