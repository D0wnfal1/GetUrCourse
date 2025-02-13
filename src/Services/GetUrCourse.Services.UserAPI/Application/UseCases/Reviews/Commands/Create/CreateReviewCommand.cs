using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Reviews.Commands.Create;

public record CreateReviewCommand(
    Guid AuthorId, 
    Guid StudentId,
    string Text, 
    int Rating) : ICommand;
