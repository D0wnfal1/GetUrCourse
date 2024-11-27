using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.ValueObjects;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Create;

public record CreateUserCommand(
    UserName Name,
    string Email,
    Role Role,
    Guid Id
) : ICommand<UserCreateResponse>;