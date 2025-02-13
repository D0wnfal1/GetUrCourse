using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Authors.Commands.Create;

public record CreateAuthorCommand(Guid UserId) : ICommand;
