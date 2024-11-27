using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Queries.GetById;
using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Queries.GetByStudentId;

public record GetCertificatesByStudentIdQuery(
    Guid StudentId,
    string? SearchTerm,
    string? SortColumn,
    string? OrderBy,
    int PageNumber,
    int PageSize) : IQuery<PagedList<CertificateMainResponse>>;

