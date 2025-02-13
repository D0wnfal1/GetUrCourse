using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Commands.Delete;

public record DeleteManagerCommand(Guid Id) : ICommand;
