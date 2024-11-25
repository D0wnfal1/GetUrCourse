using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Title)
            .NotEmptyAndNotLongerThan(
                nameof(Category.Title),
                Category.MaxCategoryTitleLength
            );

        RuleFor(c => c.Description)
            .NotEmptyAndNotLongerThan(
                nameof(Category.Description),
                Category.MaxCategoryDescriptionLength
            );
    } 
}