using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Validators;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(a => a.FullName)
            .NotEmptyAndNotLongerThan(
                nameof(Author.FullName), 
                Author.MaxAuthorFullNameLength);

        RuleFor(a => a.ImageUrl)
            .NotEmptyAndNotLongerThan(nameof(Author.ImageUrl), Author.MaxAuthorImageUrlLength);
    }
}