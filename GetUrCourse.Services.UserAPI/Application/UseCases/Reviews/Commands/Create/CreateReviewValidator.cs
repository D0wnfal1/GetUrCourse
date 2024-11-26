using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Reviews.Commands.Create;

public class CreateReviewValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewValidator(UserDbContext context)
    {
        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("AuthorId is required.")
            .IsAuthorExist(context);

        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("StudentId is required.")
            .IsStudentExist(context);

        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Text is required.")
            .MaximumLength(Review.MaxTextLength);

        RuleFor(x => x.Rating)
            .NotEmpty()
            .WithMessage("Rating is required.")
            .InclusiveBetween(Review.MinRating, Review.MaxRating)
            .WithMessage($"Rating must be between {Review.MinRating} and {Review.MaxRating}.");
    }
}