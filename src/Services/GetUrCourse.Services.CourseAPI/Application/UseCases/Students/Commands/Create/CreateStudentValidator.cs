using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.Create;

public class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentValidator(CourseDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Student.Id)))
            .IsStudentExist(context);

        RuleFor(x => x.FullName)
            .NotEmptyAndNotLongerThan(nameof(Student.FullName), Student.MaxFullNameLength);
        
        RuleFor(x => x.ImageUrl)
            .MaximumLength(Student.MaxImageUrlLength);
    }
}