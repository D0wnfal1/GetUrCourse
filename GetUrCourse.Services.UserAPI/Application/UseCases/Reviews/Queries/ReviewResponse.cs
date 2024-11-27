namespace GetUrCourse.Services.UserAPI.Application.UseCases.Reviews.Queries;

public record ReviewResponse(
    Guid Id, 
    Guid StudentId,
    string StudentImage,
    string StudentName,
    string Text, 
    int Rating, 
    DateTime CreatedAt);
