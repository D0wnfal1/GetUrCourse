using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Authors.Queries.GetById;

public class GetAuthorByIdValidator : AbstractValidator<GetAuthorByIdQuery>
{
    public GetAuthorByIdValidator(UserDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("AuthorId is required")
            .IsAuthorExist(context);
    }
}