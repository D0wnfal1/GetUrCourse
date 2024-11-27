using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Enums;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Queries.GetCourse;

public class GetCourseByIdQueryHandler(CourseDbContext context) : IQueryHandler<GetCourseByIdQuery, CourseFullResponse>
{
    public async Task<Result<CourseFullResponse>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await context.Courses
            .AsNoTracking()
            .Where(c => c.Id == request.Id)
            .Select(c => new CourseFullResponse(
                    c.Id,
                    c.Title,
                    c.Subtitle,
                    c.FullDescription,
                    c.Requirements,
                    c.ImageUrl,
                    c.Rating.Value,
                    c.Price,
                    c.DiscountPrice,
                    c.TotalDuration.TotalHours,
                    Enum.GetName(typeof(Language), c.Language) ?? "unknown",
                    Enum.GetName(typeof(Level), c.Level) ?? "unknown",
                    c.HasHomeTask,
                    c.HasPossibilityToContactTheTeacher,
                    c.CreatedAt,
                    c.IsUpdated,
                    c.UpdatedAt,
                    c.CountOfStudents))
            .FirstOrDefaultAsync(cancellationToken);
        
        if (course is null)
            return Result.Failure<CourseFullResponse>(new Error("get_course", "Course not found."));
        
        return Result.Success(course);
    }
}