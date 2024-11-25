using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Create;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
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