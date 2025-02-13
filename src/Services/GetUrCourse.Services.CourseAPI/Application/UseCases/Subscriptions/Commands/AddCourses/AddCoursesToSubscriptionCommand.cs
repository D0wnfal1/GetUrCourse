using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.AddCourses;

public record AddCoursesToSubscriptionCommand(int SubscriptionId, IEnumerable<Guid> CourseIds): ICommand;
