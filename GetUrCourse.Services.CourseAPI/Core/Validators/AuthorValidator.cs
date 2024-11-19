using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;

namespace GetUrCourse.Services.CourseAPI.Core.Validators;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(a => a.FullName)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Author.FullName)))
            .MaximumLength(Author.MaxAuthorFullNameLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(Author.FullName), Author.MaxAuthorFullNameLength));

        RuleFor(a => a.ShortDescription)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Author.ShortDescription)))
            .MaximumLength(Author.MaxAuthorShortDescriptionLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(Author.ShortDescription), Author.MaxAuthorShortDescriptionLength));

        RuleFor(a => a.ImageUrl)
            .NotEmpty()
            .WithMessage("Author image URL should not be empty");
    }
}