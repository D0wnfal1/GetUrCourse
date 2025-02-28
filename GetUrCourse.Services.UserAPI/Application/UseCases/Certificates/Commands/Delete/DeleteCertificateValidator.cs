using FluentValidation;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Commands.Delete;

public class DeleteCertificateValidator : AbstractValidator<DeleteCertificateCommand>
{
    public DeleteCertificateValidator(UserDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("CertificateId is required")
            .MustAsync(async (id, cancellationToken) => 
                await context.Certificates.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken))
            .WithMessage("Certificate not found");

    }
}