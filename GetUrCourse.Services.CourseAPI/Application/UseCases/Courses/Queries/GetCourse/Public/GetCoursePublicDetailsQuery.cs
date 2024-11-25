using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Contracts;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Queries.GetCourse.Public;

public record GetCoursePublicDetailsQuery(
    Guid CourseId
    ) : IQuery<CourseResponse>;
