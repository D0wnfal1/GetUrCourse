namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Queries;

public record StudentShortResponse(
    Guid Id,
    string ImageUrl,
    string Name);

