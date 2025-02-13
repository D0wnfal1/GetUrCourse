using GetUrCourse.Services.CourseAPI.Shared;
using MediatR;

namespace GetUrCourse.Services.CourseAPI.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
public interface ICachedQuery
{
    string CacheKey { get; }
    TimeSpan? Expiration { get; }
} 