using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;

namespace GetUrCourse.Services.CourseAPI.Core.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Category.Title)))
            .MaximumLength(Category.MaxCategoryTitleLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(Category.Title), Category.MaxCategoryTitleLength));
        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Category.Description)))
            .MaximumLength(Category.MaxCategoryDescriptionLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(Category.Description), Category.MaxCategoryDescriptionLength));
        
    } 
}