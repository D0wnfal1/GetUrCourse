using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Queries.GetCoursesBySubscription;

public record GetCoursesBySubscriptionQuery(
    int SubscriptionId,
    int PageNumber,
    int PageSize) : IQuery<PagedList<CourseShortResponse>>;
