namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Queries;

public record SubscriptionResponse(int Id, string Name, string Description, decimal Price, decimal DiscountPrice);
