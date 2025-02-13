using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Queries.GetById;

public class GetCertificateByIdQueryHandler(UserDbContext context) : IQueryHandler<GetCertificateByIdQuery, CertificateMainResponse>
{
    public async Task<Result<CertificateMainResponse>> Handle(GetCertificateByIdQuery request, CancellationToken cancellationToken)
    {
        var certificate = await context.Certificates
            .AsNoTracking()
            .Where(c => c.Id == request.Id)
            .Select(c => new CertificateMainResponse(
                c.Id,
                c.Title,
                c.Description,
                c.PdfUrl,
                c.Date))
            .FirstOrDefaultAsync(cancellationToken);

        if (certificate is null)
        {
            return Result.Failure<CertificateMainResponse>(
                new Error("get_certificate", "Certificate not found"));
        }
        
        return Result.Success(certificate);
    }
}