using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Commands.Create;

public class CreateCertificateCommandHandler(UserDbContext context) : ICommandHandler<CreateCertificateCommand>
{
    public async Task<Result> Handle(CreateCertificateCommand request, CancellationToken cancellationToken)
    {
        var student = await context.Students.FindAsync(request.StudentId);
        
        var certificate = Certificate.Create(
            request.Title, 
            request.Description, 
            request.PdfUrl, 
            request.StudentId);

        if (certificate.IsFailure)
            return Result.Failure(certificate.Error);
        
        var result = student!.AddCertificate(certificate.Value);
        if (result.IsFailure)
            return Result.Failure(result.Error);
        
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}