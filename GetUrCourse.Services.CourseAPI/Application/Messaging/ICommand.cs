using GetUrCourse.Services.CourseAPI.Shared;
using MediatR;

namespace GetUrCourse.Services.CourseAPI.Application.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
