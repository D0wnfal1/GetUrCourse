using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Commands.Create;

public class CreateModuleValidator : AbstractValidator<CreateModuleCommand>
{
    public CreateModuleValidator(CourseDbContext context)
    {
        RuleFor(x => x.Title)
            .NotEmptyAndNotLongerThan(nameof(CourseModule.Title), CourseModule.MaxTitleLength);
        
        RuleFor(x => x.Description)
            .NotEmptyAndNotLongerThan(nameof(CourseModule.Description), CourseModule.MaxDescriptionLength);
        
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(CourseModule.CourseId)))
            .IsCourseExist(context);
    }
}