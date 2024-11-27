using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.AddSubscription;

public class AddSubscriptionToStudentValidator: AbstractValidator<AddSubscriptionToStudentCommand>
{
    public AddSubscriptionToStudentValidator(CourseDbContext context)
    {
        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Student.Id)))
            .IsStudentExist(context);

        RuleFor(x => x.SubscriptionId)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(Subscription.Id)))
            .IsSubscriptionExist(context);

        RuleFor(x => x.Duration)
            .GreaterThan(1)
            .LessThan(12)
            .WithMessage("Duration should be between 1 and 12 months")
            .When(x => x.Duration is not null);
    }
}