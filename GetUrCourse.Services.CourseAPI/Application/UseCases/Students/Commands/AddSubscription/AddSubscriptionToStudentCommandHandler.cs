using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.AddSubscription;

public class AddSubscriptionToStudentCommandHandler(CourseDbContext context) : ICommandHandler<AddSubscriptionToStudentCommand>
{
    public async Task<Result> Handle(AddSubscriptionToStudentCommand request, CancellationToken cancellationToken)
    {
        var studentSubscription = StudentSubscription.Create(request.Id, request.SubscriptionId, request.Duration);

        if (studentSubscription.IsFailure)
        {
            return Result.Failure<AddSubscriptionToStudentCommand>(new Error("add_subscription", "Failed to create student subscription."));
        }
      
        await context.AddAsync(studentSubscription.Value!, cancellationToken);
    
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}