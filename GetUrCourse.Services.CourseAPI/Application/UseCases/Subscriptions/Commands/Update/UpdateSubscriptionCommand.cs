using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.Update;

public record UpdateSubscriptionCommand(
    int Id, 
    string? Title, 
    string? Description,
    decimal Price,
    decimal DiscountPrice) : ICommand;
