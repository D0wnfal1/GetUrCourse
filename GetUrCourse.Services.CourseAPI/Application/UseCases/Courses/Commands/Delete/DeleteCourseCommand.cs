using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.Delete;

public record DeleteCourseCommand(Guid Id) : ICommand;
