using FluentValidation;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Reviews.Queries.GetById;

public class GetReviewByIdValidator : AbstractValidator<GetReviewByIdQuery>
{
    public GetReviewByIdValidator(UserDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("StudentId is required")
            .MustAsync(async (id, token) =>
                await context.Reviews.AnyAsync(r => r.Id == id, cancellationToken: token))
            .WithMessage("Review not found");
    }
}