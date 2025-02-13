using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.AddCourses;

public class AddCoursesToSubscriptionCommandHandler(CourseDbContext context) : ICommandHandler<AddCoursesToSubscriptionCommand>
{
    public async Task<Result> Handle(AddCoursesToSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await context.Subscriptions
            .Where(a => a.Id == request.SubscriptionId)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (subscription == null)
        {
            return Result.Failure(new Error("add_courses", "Subscription not found"));
        }
        
        var courses = await context.Courses
            .Where(a => request.CourseIds.Contains(a.Id))
            .ToListAsync(cancellationToken);
        
        if (courses.Count != request.CourseIds.Count())
        {
            return Result.Failure(new Error("add_courses", "Some courses not found"));
        }
        
        subscription.AddCourses(courses);
        
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}