using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.Delete;

public record DeleteSubscriptionCommand(int Id) : ICommand;
