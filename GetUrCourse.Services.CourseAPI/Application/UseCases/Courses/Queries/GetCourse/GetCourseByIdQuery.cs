using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Queries.GetCourse;

public record GetCourseByIdQuery(Guid Id) : IQuery<CourseFullResponse>;
