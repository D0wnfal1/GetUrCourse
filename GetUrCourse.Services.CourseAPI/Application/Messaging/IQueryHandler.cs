using GetUrCourse.Services.CourseAPI.Shared;
using MediatR;

namespace GetUrCourse.Services.CourseAPI.Application.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
    
}