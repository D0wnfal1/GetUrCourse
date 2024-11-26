using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Validators;

public class CourseModuleValidator : AbstractValidator<CourseModule>
{
    public CourseModuleValidator()
    {
        RuleFor(c => c.Title)
            .NotEmptyAndNotLongerThan(
                nameof(CourseModule.Title),
                CourseModule.MaxTitleLength
            );

        RuleFor(c => c.Description)
            .NotEmptyAndNotLongerThan(
                nameof(CourseModule.Description),
                CourseModule.MaxDescriptionLength
            );
        
        RuleFor(c => c.PdfUrl)
            .NotEmptyAndNotLongerThan(
                nameof(CourseModule.PdfUrl),
                CourseModule.MaxPdfUrlLength
            );
        
    }
}