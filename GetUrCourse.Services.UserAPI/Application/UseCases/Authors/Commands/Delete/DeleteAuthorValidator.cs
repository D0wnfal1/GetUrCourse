using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Authors.Commands.Delete;

public class DeleteAuthorValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorValidator(UserDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .IsAuthorExist(context);
    }
}