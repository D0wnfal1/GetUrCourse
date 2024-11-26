using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Core.Models;

public class Student 
{
    private readonly IList<Certificate> _certificates = [];

    private Student(Guid userId)
    {
        UserId = userId;
        CoursesInProgress = 0;
        CoursesCompleted = 0;
    }
    
    public Guid UserId { get; set; }
    public int CoursesInProgress { get; set; }
    public int CoursesCompleted { get; set; }
    public IReadOnlyCollection<Certificate> Certificates => _certificates.AsReadOnly();
    
    public virtual User User { get; init; }
    public virtual ICollection<Review> Reviews { get;  set; }
    
    public static Result<Student> Create(Guid userId)
    {
        return Result.Success(new Student(userId));
    }
    
    public Result Update(int? coursesInProgress, int? coursesCompleted)
    {
        if (coursesInProgress.HasValue && CoursesInProgress != coursesInProgress.Value)
        {
            CoursesInProgress = coursesInProgress.Value;
        }

        if (coursesCompleted.HasValue && CoursesCompleted != coursesCompleted.Value)
        {
            CoursesCompleted = coursesCompleted.Value;
        }

        return Result.Success();
    }
    
    public Result AddCertificate(Certificate certificate)
    {
        if (_certificates.Any(c => c.Id == certificate.Id))
        {
            return Result.Failure(new Error("add_certificate", "Certificate already exists"));
        }
        
        _certificates.Add(certificate);
        return Result.Success();
    }
    
    public Result RemoveCertificate(Guid certificateId)
    {
        var certificate = _certificates.FirstOrDefault(c => c.Id == certificateId);
        if (certificate is null)
        {
            return Result.Failure(new Error("remove_certificate", "Certificate not found"));
        }
        
        _certificates.Remove(certificate);
        return Result.Success();
    }
    
    


}