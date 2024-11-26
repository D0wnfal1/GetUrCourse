using FluentValidation;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Queries.GetById;

public class GetManagerByIdValidator : AbstractValidator<GetManagerByIdQuery>
{
    public GetManagerByIdValidator(UserDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ManagerId is required")
            .MustAsync(async (managerId, cancellationToken) =>
                await context.Managers.AnyAsync(x => x.UserId == managerId, cancellationToken))
            .WithMessage("Manager not found");
    }
}