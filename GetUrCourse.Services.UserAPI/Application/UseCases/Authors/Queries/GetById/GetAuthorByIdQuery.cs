using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Authors.Queries.GetById;

public record GetAuthorByIdQuery(Guid Id) : IQuery<AuthorMainResponse>;
