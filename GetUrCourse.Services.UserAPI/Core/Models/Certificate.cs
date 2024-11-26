using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Core.Models;

public class Certificate
{
    public const int TitleMaxLength = 100;
    public const int DescriptionMaxLength = 500;
    public const int PdfUrlMaxLength = 150;
    
    public Certificate(string title, string description, string pdfUrl, Guid studentId)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        PdfUrl = pdfUrl;
        Date = DateTime.UtcNow;
        StudentId = studentId;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string PdfUrl { get; private set; }
    public DateTime Date { get; private set; }
    public Guid StudentId { get; private set; }
    public virtual Student Student { get; init; }

    public static Result<Certificate> Create(string title, string description, string pdfUrl, Guid userId)
    {
        return Result.Success(new Certificate(title, description, pdfUrl, userId));
    }
}    