using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Commands.Create;

public class CreateCertificateValidator : AbstractValidator<CreateCertificateCommand>
{
    public CreateCertificateValidator(UserDbContext context)
    {
        RuleFor(x => x.StudentId)
            .NotEmpty()
            .WithMessage("UserId is required")
            .IsStudentExist(context);

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(Certificate.TitleMaxLength);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(Certificate.DescriptionMaxLength);

        RuleFor(x => x.PdfUrl)
            .NotEmpty()
            .WithMessage("PdfUrl is required")
            .MaximumLength(Certificate.PdfUrlMaxLength);

    }
}