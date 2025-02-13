using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.Update;

public class UpdateSubscriptionValidator : AbstractValidator<UpdateSubscriptionCommand>
{
    public UpdateSubscriptionValidator()
    {
        RuleFor(x => x.Description)
            .NotEmptyAndNotLongerThan(
                nameof(Subscription.Description), 
                Subscription.MaxSubscriptionDescriptionLength);
        
        RuleFor(x => x.Title)
            .NotEmptyAndNotLongerThan(
                nameof(Subscription.Title),
                Subscription.MaxSubscriptionTitleLength);
    }
}

