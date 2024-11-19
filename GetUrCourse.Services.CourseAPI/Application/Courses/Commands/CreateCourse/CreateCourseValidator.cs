using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.Courses.Commands.CreateCourse;

public class CreateCourseValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseValidator(CourseDbContext context)
    {
        RuleFor(x => x.Title)
            .MustAsync(async (title, token) =>
                !await context.Courses.AnyAsync(x => x.Title == title, token))
            .WithMessage("Course with this title already exists");


    }
}