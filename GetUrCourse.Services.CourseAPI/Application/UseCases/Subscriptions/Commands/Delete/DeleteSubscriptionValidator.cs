using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.Delete;

public class DeleteSubscriptionValidator :AbstractValidator<DeleteSubscriptionCommand>
{
    public DeleteSubscriptionValidator(CourseDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(DeleteSubscriptionCommand.Id)))
            .IsSubscriptionExist(context);


    }
}