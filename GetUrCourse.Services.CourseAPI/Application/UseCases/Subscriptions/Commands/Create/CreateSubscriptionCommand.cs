
using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.Create;

public record CreateSubscriptionCommand(string Title, string Description ) : ICommand;
