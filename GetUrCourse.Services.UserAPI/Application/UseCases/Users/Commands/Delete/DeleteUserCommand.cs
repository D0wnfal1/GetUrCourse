using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Delete;

public record DeleteUserCommand(Guid Id):ICommand;
