using GetUrCourse.Services.UserAPI.Core.Shared;
using MediatR;

namespace GetUrCourse.Services.UserAPI.Application.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>, IRequest<Result<TResponse>>
{
    
}