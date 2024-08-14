using FluentValidation;
using GetUrCourse.Services.CourseAPI.Application.Courses.Queries.GetCourse.Public;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.Courses.Queries.GetCourse;

public class GetCourseValidator : AbstractValidator<GetCoursePublicDetailsQuery>
{
    public GetCourseValidator(CourseDbContext context)
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .MustAsync((guid, token) => 
                context.Courses.AnyAsync(x => x.Id == guid, token))
            . WithMessage("Course not found");
    }
}