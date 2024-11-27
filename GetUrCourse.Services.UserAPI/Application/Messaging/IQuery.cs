using GetUrCourse.Services.UserAPI.Core.Shared;
using MediatR;

namespace GetUrCourse.Services.UserAPI.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

public interface ICachedQuery
{
    string CacheKey { get; }
    TimeSpan? Expiration { get; }
} 