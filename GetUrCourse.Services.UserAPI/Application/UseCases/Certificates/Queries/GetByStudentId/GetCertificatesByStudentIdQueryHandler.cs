using System.Linq.Expressions;
using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Queries.GetById;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Queries.GetByStudentId;

public class GetCertificatesByStudentIdQueryHandler(UserDbContext context) : 
    IQueryHandler<GetCertificatesByStudentIdQuery, PagedList<CertificateMainResponse>>
{
    public async Task<Result<PagedList<CertificateMainResponse>>> Handle(
        GetCertificatesByStudentIdQuery request, 
        CancellationToken cancellationToken)
    {
        IQueryable<Certificate> certificatesQuery = context.Certificates
            .AsNoTracking()
            .Where(c => c.StudentId == request.StudentId);
        
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            certificatesQuery = certificatesQuery.Where(c =>
                c.Title.Contains(request.SearchTerm));
        }
        
        certificatesQuery = request.OrderBy?.ToLower() == "desc" ? 
            certificatesQuery.OrderByDescending(GetSortProperty(request)) : 
            certificatesQuery.OrderBy(GetSortProperty(request));

        var certificatesResponseQuery = certificatesQuery
            .Select(c => new CertificateMainResponse(
                c.Id,
                c.Title,
                c.Description,
                c.PdfUrl,
                c.Date));
        
        var result = await PagedList<CertificateMainResponse>
            .CreateAsync(certificatesResponseQuery, request.PageNumber, request.PageSize);

        if (result.IsFailure)
        {
            return Result.Failure<PagedList<CertificateMainResponse>>(
                new Error("get_certificates",result.Error));
        }
        return Result.Success(result.Value!);
    }
    
    private static Expression<Func<Certificate, object>> GetSortProperty(GetCertificatesByStudentIdQuery request) => 
        request.SortColumn?.ToLower() switch
        {
            "date" => c => c.Date,
            "title" => f => f.Title,
            _ => f => f.Id
        };
}