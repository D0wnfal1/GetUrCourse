using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Reviews.Queries.GetByAutorId;

public class GetReviewsByAuthorIdValidator : AbstractValidator<GetReviewsByAuthorIdQuery>
{
    public GetReviewsByAuthorIdValidator(UserDbContext context)
    {
        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("AuthorId is required")
            .IsAuthorExist(context);
    }
}