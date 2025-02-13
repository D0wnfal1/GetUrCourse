using GetUrCourse.Services.UserAPI.Application.Messaging;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Queries.GetById;

public record GetCertificateByIdQuery(Guid Id) : IQuery<CertificateMainResponse>;