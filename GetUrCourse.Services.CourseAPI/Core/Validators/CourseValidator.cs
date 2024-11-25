using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Validators;

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        RuleFor(c => c.Title)
            .NotEmptyAndNotLongerThan(
                nameof(Course.Title),
                Course.MaxCourseTitleLength
            );

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