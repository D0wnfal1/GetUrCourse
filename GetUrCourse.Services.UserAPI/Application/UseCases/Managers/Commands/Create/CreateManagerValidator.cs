using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Commands.Create;

public class CreateManagerValidator : AbstractValidator<CreateManagerCommand>
{
    public CreateManagerValidator(UserDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .IsUserExist(context)
            .MustAsync(async (userId, cancellationToken) =>
                await context.Managers.AnyAsync(x => x.UserId != userId, cancellationToken))
            .WithMessage("Author already exists.");

        RuleFor(x => x.Department)
            .IsDepartmentValid();
    }
    
}