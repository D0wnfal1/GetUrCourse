using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Commands.Delete;

public class DeleteModuleValidator : AbstractValidator<DeleteModuleCommand>
{
    public DeleteModuleValidator(CourseDbContext context)
    {
        RuleFor(m => m.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(CourseModule.Id)))
            .IsModuleExist(context);
    }
}