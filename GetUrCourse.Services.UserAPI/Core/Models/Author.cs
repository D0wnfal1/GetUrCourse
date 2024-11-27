using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Core.Models;

public class Author
{
    private readonly List<Review> _reviews = [];
    private Author(Guid userId)
    {
        UserId = userId;
        TotalCourses = 0;
        TotalStudents = 0;
        AverageRating = 0;
    }
    
    public Guid UserId { get; private set; }
    public int TotalStudents { get; private set; }
    public int TotalReviews { get; private set; }
    public int TotalCourses { get; private set; }
    public double AverageRating { get; private set; }
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
    
    public virtual User User { get; init; }
    
    public static Result<Author> Create(Guid userId)
    {
        return Result.Success(new Author(userId));
    }
    
    public Result Update(int? totalStudents, int? totalCourses, int? totalReviews, double? averageRating)
    {
        if (totalStudents.HasValue && TotalStudents != totalStudents.Value)
        {
            TotalStudents = totalStudents.Value;
        }

        if (totalCourses.HasValue && TotalCourses != totalCourses.Value)
        {
            TotalCourses = totalCourses.Value;
        }

        if (averageRating.HasValue && AverageRating != averageRating.Value)
        {
            AverageRating = averageRating.Value;
        }

        return Result.Success();
    }
    
    
    public Result AddCourse()
    {
        TotalCourses++;
        return Result.Success();
    }
    
    public Result AddStudent()
    {
        TotalStudents++;
        return Result.Success();
    }
    
    public Result AddReview(Guid studentId, string text, int rating)
    {
        var review = Review.Create(UserId, studentId, text, rating);
        if (review.IsFailure)
            return Result.Failure(review.Error);
        _reviews.Add(review.Value);
        TotalReviews++;
        AverageRating = (AverageRating + rating) / TotalReviews;
        return Result.Success();
    }
    
    public Result RemoveReview(Guid reviewId)
    {
        var review = _reviews.FirstOrDefault(r => r.Id == reviewId);
        if (review is null)
            return Result.Failure(
                new Error("author_remove_review","Review not found"));
        
        _reviews.Remove(review);
        TotalReviews--;
        AverageRating = (AverageRating - review.Rating) / TotalReviews;
        return Result.Success();
    }

    
    public Result RemoveCourse()
    {
        if (TotalCourses == 0)
            return Result.Failure(
                new Error("author_remove_course","No courses to remove"));
        
        TotalCourses--;
        return Result.Success();
    }
    
    public Result RemoveStudent()
    {
        if (TotalStudents == 0)
            return Result.Failure(
                new Error("author_remove_student","No students to remove"));
        
        TotalStudents--;
        return Result.Success();
    }
    

}