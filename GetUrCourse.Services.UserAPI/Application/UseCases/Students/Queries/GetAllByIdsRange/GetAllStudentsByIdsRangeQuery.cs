using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Queries.GetAllByIdsRange;

public record GetAllStudentsByIdsRangeQuery(Guid[] StudentsIds)
    : IQuery<PagedList<StudentShortResponse>>;
