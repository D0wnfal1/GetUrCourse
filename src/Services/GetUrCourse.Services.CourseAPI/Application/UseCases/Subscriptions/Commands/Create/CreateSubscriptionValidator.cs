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

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price should be greater than 0")
            .GreaterThanOrEqualTo(x => x.DiscountPrise)
            .WithMessage("Price should be greater than or equal to the discount price");
        
        RuleFor(x => x.DiscountPrise)
            .LessThanOrEqualTo(x => x.Price)
            .WithMessage("Discount price should be less than or equal to the price")
            .When(x => x.DiscountPrise.HasValue);


    }
}