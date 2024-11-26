using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.Update;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseValidator(CourseDbContext context)
    {
        RuleFor(c => c.Title)
            .NotEmptyAndNotLongerThan(
                nameof(Course.Title),
                Course.MaxCourseTitleLength
            )
            .MustAsync(async (title, token) =>
                !await context.Courses.AnyAsync(x => x.Title == title, token))
            .When(x => x.Title != string.Empty);

        RuleFor(c => c.Subtitle)
            .NotEmptyAndNotLongerThan(
                nameof(Course.Subtitle),
                Course.MaxCourseSubtitleLength
            )
            .When(x => x.Subtitle != string.Empty);

        RuleFor(c => c.FullDescription)
            .NotEmptyAndNotLongerThan(
                nameof(Course.FullDescription),
                Course.MaxCourseFullDescriptionLength
            )
            .When(x => x.FullDescription != string.Empty);

        RuleFor(c => c.Requirements)
            .NotEmptyAndNotLongerThan(
                nameof(Course.Requirements),
                Course.MaxCourseRequirementsLength
            )
            .When(x => x.Requirements != string.Empty);

        RuleFor(c => c.ImageUrl)
            .NotEmptyAndNotLongerThan(
                nameof(Course.ImageUrl),
                Course.MaxCourseImageUrlLength
            )
            .When(x => x.ImageUrl != string.Empty);

        RuleFor(c => c.Price)
            .LessThan(0)
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