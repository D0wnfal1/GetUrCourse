using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Authors.Commands.Delete;

public class DeleteAuthorValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorValidator(CourseDbContext context)
    {
        RuleFor(a => a.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Author.Id)))
            .IsAuthorExist(context);
    }
}