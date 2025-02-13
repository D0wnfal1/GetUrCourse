using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Queries.GetByEmail;

public record GetUserByEmailQuery(string Email) : IQuery<UserShortResponse>;