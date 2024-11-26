using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Authors.Commands.Create;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorValidator(UserDbContext context)
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.")
            .IsUserExist(context)
            .MustAsync(async (userId, cancellationToken) =>
                await context.Authors.AnyAsync(x => x.UserId != userId, cancellationToken))
            .WithMessage("Author already exists.");
    }
}