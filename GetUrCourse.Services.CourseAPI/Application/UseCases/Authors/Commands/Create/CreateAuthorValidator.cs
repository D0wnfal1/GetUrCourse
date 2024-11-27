using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Authors.Commands.Create;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorValidator(CourseDbContext context)
    {
        RuleFor(a => a.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Author.Id)))
            .MustAsync(async (id, token) =>
                !await context.Authors.AnyAsync(a => a.Id == id, cancellationToken: token))
            .WithMessage("Author exist");
        
        RuleFor(a => a.FullName)
            .NotEmptyAndNotLongerThan(
                nameof(Author.FullName), 
                Author.MaxAuthorFullNameLength);

        RuleFor(a => a.ImageUrl)
            .NotEmptyAndNotLongerThan(
                nameof(Author.ImageUrl),
                Author.MaxAuthorImageUrlLength);
    }
}