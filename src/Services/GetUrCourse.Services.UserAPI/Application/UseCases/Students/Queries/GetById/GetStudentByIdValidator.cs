using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Queries.GetById;

public class GetStudentByIdValidator : AbstractValidator<GetStudentByIdQuery>
{
    public GetStudentByIdValidator(UserDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("StudentId is required")
            .IsStudentExist(context);
    }
}