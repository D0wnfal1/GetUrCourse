using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Queries.GetByStudentId;

public class GetCertificatesByStudentIdValidator : AbstractValidator<GetCertificatesByStudentIdQuery>
{
    public GetCertificatesByStudentIdValidator(UserDbContext context)
    {
        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("StudentId is required")
            .IsStudentExist(context);
    }
}