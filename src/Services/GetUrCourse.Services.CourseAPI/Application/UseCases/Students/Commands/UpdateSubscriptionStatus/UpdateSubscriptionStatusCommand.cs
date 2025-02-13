using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Enums;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.UpdateSubscriptionStatus;

public record UpdateSubscriptionStatusCommand(
    Guid StudentId, 
    int SubscriptionId, 
    SubscriptionStatus Status) : ICommand;
