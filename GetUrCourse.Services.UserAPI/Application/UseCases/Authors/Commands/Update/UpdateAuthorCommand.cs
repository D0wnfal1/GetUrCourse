using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Authors.Commands.Update;

public record UpdateAuthorCommand(
    Guid UserId, 
    int TotalCourses, 
    int TotalStudents, 
    double AverageRating ) : ICommand;
