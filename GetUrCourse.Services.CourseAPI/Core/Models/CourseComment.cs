using CSharpFunctionalExtensions;
using GetUrCourse.Services.CourseAPI.Core.Validators;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class CourseComment
{
    public const int MaxCommentLength = 1000;

    private CourseComment(Guid courseId, Guid studentId, string text)
    {
        Id = Guid.NewGuid();
        CourseId = courseId;
        StudentId = studentId;
        Text = text;
        CreatedAt = DateTime.UtcNow;
        IsUpdated = false;
    }
    
    public Guid Id { get; private set; }
    public Guid CourseId { get; private set; }
    public virtual Course Course { get; private set; }
    public Guid StudentId { get; private set; }
    public virtual Student Student { get; private set; }
    public string Text { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsUpdated { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private static Result<CourseComment> Validate(CourseComment comment)
    {
        var validator = new CourseCommentValidator();
        var result = validator.Validate(comment);
        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            return Result.Failure<CourseComment>(errors);
        }
        return Result.Success(comment);
    }
    
    public static Result<CourseComment> Create(Guid courseId, Guid studentId, string text)
    {
        var comment = new CourseComment(courseId, studentId, text);
        return Validate(comment);
    }
    
    public Result Update(string text)
    {
        IsUpdated = true;
        Text = text;
        UpdatedAt = DateTime.UtcNow;
        
        var validationResult = Validate(this);
        return validationResult.IsFailure ? validationResult : Result.Success();
    }
    
    
    
    
}