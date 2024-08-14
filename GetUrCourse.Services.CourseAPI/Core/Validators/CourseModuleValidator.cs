using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;

namespace GetUrCourse.Services.CourseAPI.Core.Validators;

public class CourseModuleValidator : AbstractValidator<CourseModule>
{
    public CourseModuleValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(CourseModule.Title)))
            .MaximumLength(CourseModule.MaxTitleLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(CourseModule), CourseModule.MaxTitleLength));

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(CourseModule.Description)))
            .MaximumLength(CourseModule.MaxDescriptionLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(CourseModule.Description), CourseModule.MaxDescriptionLength));
        
        RuleFor(c => c.PdfUrl)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(CourseModule.PdfUrl)));
        
    }
}