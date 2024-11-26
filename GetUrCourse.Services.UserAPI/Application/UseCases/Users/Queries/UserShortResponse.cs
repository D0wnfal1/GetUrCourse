namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Queries;

public record UserShortResponse(
    Guid Id,
    string ImageUrl,
    string Name);
