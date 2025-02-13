using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Queries.GetById;

public record GetStudentByIdQuery(Guid Id) : IQuery<StudentFullResponse>;
