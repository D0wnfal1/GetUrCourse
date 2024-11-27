using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Reviews.Commands.Delete;

public record DeleteReviewCommand(Guid Id) : ICommand;
