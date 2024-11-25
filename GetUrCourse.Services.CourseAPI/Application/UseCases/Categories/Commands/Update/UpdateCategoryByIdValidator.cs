using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Update;

public class UpdateCategoryByIdValidator : AbstractValidator<UpdateCategoryByIdCommand>
{
    public UpdateCategoryByIdValidator(CourseDbContext context)
    {
        RuleFor(c => c.Title)
            .MaximumLength(Category.MaxCategoryTitleLength);
        
        RuleFor(c => c.Description)
            .MaximumLength(Category.MaxCategoryDescriptionLength);
    }
}