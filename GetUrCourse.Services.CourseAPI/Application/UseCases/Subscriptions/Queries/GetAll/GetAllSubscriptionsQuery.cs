using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Models;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Queries.GetAll;

public record GetAllSubscriptionsQuery() : IQuery<IEnumerable<SubscriptionResponse>>;
