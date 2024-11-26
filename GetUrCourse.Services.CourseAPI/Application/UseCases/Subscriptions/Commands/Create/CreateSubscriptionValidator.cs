using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.Create;

public class CreateSubscriptionValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmptyAndNotLongerThan(
                nameof(Subscription.Title), 
                Subscription.MaxSubscriptionTitleLength);

        RuleFor(x => x.Description)
            .NotEmptyAndNotLongerThan(
                nameof(Subscription.Description), 
                Subscription.MaxSubscriptionDescriptionLength);
    }
}