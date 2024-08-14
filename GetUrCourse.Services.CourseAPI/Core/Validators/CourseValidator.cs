using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Enums;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;

namespace GetUrCourse.Services.CourseAPI.Core.Validators;

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
         RuleFor(c => c.Title)
            .NotEmpty().WithMessage(DomainExceptions.Empty(nameof(Course.Title)))
            .MaximumLength(Course.MaxCourseTitleLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(Course.Title), Course.MaxCourseTitleLength));

        RuleFor(c => c.Subtitle)
            .NotEmpty().WithMessage(DomainExceptions.Empty(nameof(Course.Subtitle)))
            .MaximumLength(Course.MaxCourseSubtitleLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(Course.Subtitle), Course.MaxCourseSubtitleLength));

        RuleFor(c => c.FullDescription)
            .NotEmpty().WithMessage(DomainExceptions.Empty(nameof(Course.FullDescription)))
            .MaximumLength(Course.MaxCourseFullDescriptionLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(Course.FullDescription), Course.MaxCourseFullDescriptionLength));

        RuleFor(c => c.Requirements)
            .NotEmpty().WithMessage(DomainExceptions.Empty(nameof(Course.Requirements)))
            .MaximumLength(Course.MaxCourseRequirementsLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(Course.Requirements), Course.MaxCourseRequirementsLength));

        RuleFor(c => c.ImageUrl)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Course.ImageUrl)));

        RuleFor(c => c.Price)
            .LessThan(0)
            .WithMessage(DomainExceptions.Empty(nameof(Course.Price)));

        RuleFor(c => c.DiscountPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Discount price should not be negative");

        RuleFor(c => c.Language)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Course.Language)))
            .MaximumLength(Course.MaxCourseLanguageLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(Course.Language), Course.MaxCourseLanguageLength));

        RuleFor(c => c.Level)
            .Must(level => Enum.IsDefined(typeof(Levels), level))
            .WithMessage("Invalid level value");
           
    }
}