using GetUrCourse.Services.UserAPI.Core.Shared;
using MediatR;

namespace GetUrCourse.Services.UserAPI.Application.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
    
}
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse> 
{
    
}