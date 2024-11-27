using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Queries.GetAll;

public class GetAllManagersValidator : AbstractValidator<GetAllManagersQuery>
{
    public GetAllManagersValidator()
    {
        RuleFor(x => x.SearchTerm)
            .MaximumLength(50)
            .WithMessage("SearchTerm must not exceed 50 characters");

        RuleFor(x => x.Department)
            .IsDepartmentValid()
            .When(x => x.Department.HasValue);
    }   
}