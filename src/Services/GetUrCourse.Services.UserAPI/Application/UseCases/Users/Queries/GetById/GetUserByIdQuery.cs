using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Queries.GetById;

public record GetUserByIdQuery(Guid Id) : IQuery<UserFullResponse>;
