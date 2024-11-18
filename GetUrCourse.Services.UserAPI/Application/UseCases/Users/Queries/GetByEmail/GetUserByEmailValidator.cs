using FluentValidation;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Queries.GetByEmail;

public class GetUserByEmailValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailValidator(UserDbContext context)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(async (email, token) =>
                await context.Users.AnyAsync(u => u.Email == email, cancellationToken: token))
            .WithMessage("User not found");
    }
}