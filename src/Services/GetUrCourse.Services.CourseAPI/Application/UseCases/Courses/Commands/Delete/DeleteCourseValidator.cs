using FluentValidation;
using GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Commands.Delete;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.Delete;

public class DeleteCourseValidator : AbstractValidator<DeleteModuleCommand>
{
    public DeleteCourseValidator(CourseDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Course.Id)))
            .IsCourseExist(context);
    }
}