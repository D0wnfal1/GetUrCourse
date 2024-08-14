using CSharpFunctionalExtensions;
using MediatR;

namespace GetUrCourse.Services.CourseAPI.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}