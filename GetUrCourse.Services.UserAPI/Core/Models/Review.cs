using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Core.Models;

public class Review
{
    
    public const int MaxRating = 5;
    public const int MinRating = 1;
    public const int MaxTextLength = 500;
    
    private Review(Guid authorId, Guid studentId, string text, int rating)
    {
        Id = Guid.NewGuid();
        AuthorId = authorId;
        StudentId = studentId;
        Text = text;
        Rating = rating;
        CreatedAt = DateTime.UtcNow;
    }
    public Guid Id { get; private set; }
    public Guid AuthorId { get; private set; }
    public Guid StudentId { get; private set; }
    public string Text { get; private set; }
    
    public int Rating { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public static Result<Review> Create(Guid authorId, Guid studentId, string text, int rating)
    {
        var review = new Review(authorId, studentId, text, rating);
        return Result.Success(review);
    }
    
    
    public virtual Student Student { get; private set; }
    public virtual Author Author { get; private set; }
}