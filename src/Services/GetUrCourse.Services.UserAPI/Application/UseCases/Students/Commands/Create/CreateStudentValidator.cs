using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Commands.Create;

public class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentValidator(UserDbContext context)
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required")
            .IsUserExist(context)
            .MustAsync(async (id, cancellationToken) =>
                !await context.Students.AnyAsync(x => x.UserId == id, cancellationToken))
            .WithMessage("Student already exists");



    }
}