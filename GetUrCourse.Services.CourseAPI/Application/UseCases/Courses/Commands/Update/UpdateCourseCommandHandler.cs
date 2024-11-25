using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.Update;

public class UpdateCourseCommandHandler(CourseDbContext context) : ICommandHandler<UpdateCourseCommand>
{
    public async Task<Result> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await context.Courses
            .Where(c => c.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (course is null)
        {
            return Result.Failure(
                new Error(
                    "update_course", 
                    "Course not found"));
        }
        
        course.Update(
            request.Title,
            request.Subtitle,
            request.FullDescription,
            request.Requirements,
            request.ImageUrl,
            request.Price,
            request.DiscountPrice,
            request.Language,
            request.Level,
            request.CategoryId);
        
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}