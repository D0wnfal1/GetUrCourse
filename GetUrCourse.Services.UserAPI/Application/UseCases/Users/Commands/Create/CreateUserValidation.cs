using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Core.ValueObjects;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Create;

public class CreateUserValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidation(UserDbContext context)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is not valid")
            .IsEmailUnique(context);

        RuleFor(x => x.Role)
            .IsRoleValid();
            
    }
}