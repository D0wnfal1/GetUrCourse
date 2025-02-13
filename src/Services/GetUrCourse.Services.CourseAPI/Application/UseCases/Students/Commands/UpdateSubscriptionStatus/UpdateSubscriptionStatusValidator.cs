using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.UpdateSubscriptionStatus;

public class UpdateSubscriptionStatusValidator : AbstractValidator<UpdateSubscriptionStatusCommand>
{
    public UpdateSubscriptionStatusValidator(CourseDbContext context)
    {
        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Student.Id)))
            .IsStudentExist(context);

        RuleFor(x => x.SubscriptionId)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Subscription.Id)))
            .IsSubscriptionExist(context);

        RuleFor(x => x.Status)
            .IsStatusValid();



    }
}