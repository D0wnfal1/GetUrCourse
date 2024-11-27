using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Queries.GetById;

public record GetSubscriptionByIdQuery(int Id) : IQuery<SubscriptionResponse>;
