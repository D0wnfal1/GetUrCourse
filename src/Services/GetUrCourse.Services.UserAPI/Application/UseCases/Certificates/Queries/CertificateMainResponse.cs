namespace GetUrCourse.Services.UserAPI.Application.UseCases.Certificates.Queries.GetById;

public sealed record CertificateMainResponse(
    Guid Id,
    string Title,
    string Description,
    string PdfUrl,
    DateTime Date)
{

    public CertificateMainResponse(
        Guid id,
        string title,
        string description,
        Uri pdfUrl,
        DateTime date)
        : this(id, title, description, pdfUrl.ToString(), date)
    {
    }
}