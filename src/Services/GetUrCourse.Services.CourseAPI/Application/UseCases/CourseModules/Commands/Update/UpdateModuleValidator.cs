using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Commands.Update;

public class UpdateModuleValidator : AbstractValidator<UpdateModuleCommand>
{
    public UpdateModuleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(CourseModule.Id)));
        
        RuleFor(x => x.Title)
            .MaximumLength(CourseModule.MaxTitleLength)
            .When(x => x.Title != null);
        
        RuleFor(x => x.Description)
            .MaximumLength(CourseModule.MaxDescriptionLength)
            .When(x => x.Description != null);
        
        RuleFor(x => x.PdfUrl)
            .MaximumLength(CourseModule.MaxPdfUrlLength)
            .When(x => x.PdfUrl != null);
    }
}