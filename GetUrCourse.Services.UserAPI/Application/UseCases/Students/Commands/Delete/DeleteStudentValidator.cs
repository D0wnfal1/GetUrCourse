using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Commands.Delete;

public class DeleteStudentValidator : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentValidator(UserDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required")
            .IsStudentExist(context);
    }
}