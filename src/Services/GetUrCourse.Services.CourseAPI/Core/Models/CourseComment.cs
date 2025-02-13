using GetUrCourse.Services.CourseAPI.Core.ValueObjects;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class CourseComment
{
    public const int MaxCommentLength = 1000;

    private CourseComment(Guid courseId, Guid studentId, string text, int rating)
    {
        Id = Guid.NewGuid();
        CourseId = courseId;
        StudentId = studentId;
        Text = text;
        CreatedAt = DateTime.UtcNow;
        IsUpdated = false;
        Rating = rating;
    }
    
    public Guid Id { get; private set; }
    public Guid CourseId { get; private set; }
    public virtual Course Course { get; set; }
    public Guid StudentId { get; private set; }
    public virtual Student Student { get; set; }
    public string Text { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsUpdated { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public int Rating { get; private set; }
    
    public static Result<CourseComment> Create(Guid courseId, Guid studentId, string text, int rating)
    {
        
        var comment = new CourseComment(courseId, studentId, text, rating);
        return Result.Success(comment);
    }
    
    public Result Update(string? text, int? rating)
    {
        IsUpdated = true;
        Text = text ?? Text;
        UpdatedAt = DateTime.UtcNow;
        
        Rating = rating ?? Rating;
        
        return Result.Success();
    }
    
    public Result UpdateRating(int rating)
    {
        Rating = rating;
        return Result.Success();
    }
    
}