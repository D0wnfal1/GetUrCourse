using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Enums;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.AddSubscription;

public record AddSubscriptionToStudentCommand(
    Guid Id, 
    int SubscriptionId,
    int? Duration) : ICommand;
