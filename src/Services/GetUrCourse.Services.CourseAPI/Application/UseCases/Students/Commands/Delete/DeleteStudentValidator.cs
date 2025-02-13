using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.Delete;

public class DeleteStudentValidator : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentValidator(CourseDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Student.Id)))
            .IsStudentExist(context);
    }
}