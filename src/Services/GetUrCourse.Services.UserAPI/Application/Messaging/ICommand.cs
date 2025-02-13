using GetUrCourse.Services.UserAPI.Core.Shared;
using MediatR;

namespace GetUrCourse.Services.UserAPI.Application.Messaging;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;

