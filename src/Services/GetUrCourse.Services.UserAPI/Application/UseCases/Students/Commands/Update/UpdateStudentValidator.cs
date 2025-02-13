using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Commands.Update;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentValidator(UserDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required")
            .IsStudentExist(context);

        RuleFor(x => x.CoursesInProgress)
            .GreaterThanOrEqualTo(0)
            .WithMessage("CoursesInProgress must be greater than or equal to 0");
        
        RuleFor(x => x.CoursesCompleted)
            .GreaterThanOrEqualTo(0)
            .WithMessage("CoursesCompleted must be greater than or equal to 0");
    }
}