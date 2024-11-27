namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Queries;

public record StudentResponse(
    Guid Id,
    string FullName,
    string ImageUrl);
