using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.RemoveCourse;

public class RemoveCourseFromSubscriptionCommandHandler(CourseDbContext context) : ICommandHandler<RemoveCourseFromSubscriptionCommand>
{
    public async Task<Result> Handle(RemoveCourseFromSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await context.Subscriptions
            .Where(a => a.Id == request.SubscriptionId)
            .FirstOrDefaultAsync(cancellationToken);

        if (subscription == null)
        {
            return Result.Failure(new Error("remove_course", "Subscription not found"));
        }
        
        var course = await context.Courses
            .Where(a => a.Id == request.CourseId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (course == null)
        {
            return Result.Failure(new Error("remove_course", "Course not found"));
        }
        
        subscription.RemoveCourse(course);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}