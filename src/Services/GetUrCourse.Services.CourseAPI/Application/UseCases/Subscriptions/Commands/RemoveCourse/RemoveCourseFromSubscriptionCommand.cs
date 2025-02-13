using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.RemoveCourse;

public record RemoveCourseFromSubscriptionCommand(int SubscriptionId, Guid CourseId) : ICommand;
