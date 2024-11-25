using FluentValidation;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.CreateCourse;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.Create;

public class CreateCourseValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseValidator(CourseDbContext context)
    {
        
        RuleFor(c => c.Title)
            .NotEmptyAndNotLongerThan(
                nameof(Course.Title),
                Course.MaxCourseTitleLength
            )
            .MustAsync(async (title, token) =>
                !await context.Courses.AnyAsync(x => x.Title == title, token))
            .WithMessage("Course with this title already exists");;

        RuleFor(c => c.Subtitle)
            .NotEmptyAndNotLongerThan(
                nameof(Course.Subtitle),
                Course.MaxCourseSubtitleLength
            );

        RuleFor(c => c.FullDescription)
            .NotEmptyAndNotLongerThan(
                nameof(Course.FullDescription),
                Course.MaxCourseFullDescriptionLength
            );

        RuleFor(c => c.Requirements)
            .NotEmptyAndNotLongerThan(
                nameof(Course.Requirements),
                Course.MaxCourseRequirementsLength
            );

        RuleFor(c => c.ImageUrl)
            .NotEmptyAndNotLongerThan(
                nameof(Course.ImageUrl),
                Course.MaxCourseImageUrlLength
            );

        RuleFor(c => c.Price)
            .GreaterThanOrEqualTo(1)
            .WithMessage(DomainExceptions.Empty(nameof(Course.Price)));

        RuleFor(c => c.DiscountPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Discount price should not be negative");

        RuleFor(c => c.Language)
            .IsLanguageValid();

        RuleFor(c => c.Level)
            .IsLevelValid();


    }
}