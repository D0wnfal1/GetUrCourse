using GetUrCourse.Services.CourseAPI.Core.Models;

namespace GetUrCourse.Services.CourseAPI.Contracts;

public class CourseResponse
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; }
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public string Level { get; set; }
    public List<string> Authors { get; set; }
    public List<CourseModule> Modules { get; set; }
    public List<CourseComment> Comments { get; set; }
    public bool HasHomeTask { get; set; }
    public bool HasPossibilityToContactTheTeacher { get; set; }
    public int CountOfStudents { get; set; }
    public int CountOfViews { get; set; }
    public bool IsUpdated { get; set; }
    public Guid CategoryId { get; set; }
    public string Category { get; set; }
    public string SubCategory { get; set; }
    public string Language { get; set; }
    public decimal DiscountPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Requirements { get; set; }
    public string Subtitle { get; set; }
    public bool IsFree { get; set; }

    
}