using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Update;

public class UpdateCategoryByIdValidator : AbstractValidator<UpdateCategoryByIdCommand>
{
    public UpdateCategoryByIdValidator(CourseDbContext context)
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Category.Id)));
        
        RuleFor(c => c.Title)
            .MaximumLength(Category.MaxCategoryTitleLength);
        
        RuleFor(c => c.Description)
            .MaximumLength(Category.MaxCategoryDescriptionLength);
    }
}