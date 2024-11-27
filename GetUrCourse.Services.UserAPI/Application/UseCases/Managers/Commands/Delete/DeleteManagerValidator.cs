using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Commands.Delete;

public class DeleteManagerValidator : AbstractValidator<DeleteManagerCommand>
{
    public DeleteManagerValidator(UserDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Guid is required.")
            .IsAuthorExist(context);
    }
}