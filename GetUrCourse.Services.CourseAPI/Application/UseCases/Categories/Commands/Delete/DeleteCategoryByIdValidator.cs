using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Delete;

public class DeleteCategoryByIdValidator : AbstractValidator<DeleteCategoryByIdCommand>
{
    public DeleteCategoryByIdValidator(CourseDbContext context)
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Category.Id)))
            .IsCategoryExist(context);
    }
}