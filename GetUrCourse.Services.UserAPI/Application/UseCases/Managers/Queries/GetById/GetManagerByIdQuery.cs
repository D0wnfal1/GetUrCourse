using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Queries.GetById;

public record GetManagerByIdQuery(Guid Id) : IQuery<ManagerResponse>;
