using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Enums;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Commands.Create;

public record CreateManagerCommand(Guid Id, Department Department) : ICommand;
