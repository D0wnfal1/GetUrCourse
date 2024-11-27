using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Models;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.Update;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        RuleFor(x => x.FullName)
            .MaximumLength(Student.MaxFullNameLength);
        RuleFor(x => x.ImageUrl)
            .MaximumLength(Student.MaxImageUrlLength);
    }
}