using CSharpFunctionalExtensions;
using GetUrCourse.Services.CourseAPI.Core.Validators;
using GetUrCourse.Services.CourseAPI.Core.ValueObjects;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class Course
{
    public const int MaxCourseTitleLength = 100;
    public const int MaxCourseSubtitleLength = 200;
    public const int MaxCourseFullDescriptionLength = 2000;
    public const int MaxCourseRequirementsLength = 1000;
    public const int MaxCourseLanguageLength = 50;
    public const int MaxCourseLevelLength = 50;
    
    private readonly List<Author> _authors = new();
    private readonly List<CourseModule> _modules = new();
    private readonly List<CourseComment> _comments = new();
    
    private Course(
        string title, 
        string subtitle, 
        string fullDescription, 
        string requirements, 
        string imageUrl, 
        decimal price, 
        decimal discountPrice, 
        string language, 
        int level,
        bool hasHomeTask,
        bool hasPossibilityToContactTheTeacher,
        Guid categoryId
        )
    {
        Id = Guid.NewGuid();
        Title = title;
        Subtitle = subtitle;
        FullDescription = fullDescription;
        Requirements = requirements;
        ImageUrl = imageUrl;
        Rating = Rating.Create();
        Price = price;
        DiscountPrice = discountPrice;
        Language = language;
        Level = level;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        HasHomeTask = hasHomeTask;
        HasPossibilityToContactTheTeacher = hasPossibilityToContactTheTeacher;
        IsUpdated = false;
        CountOfStudents = 0;
        CountOfViews = 0;
        CategoryId = categoryId;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Subtitle { get; private set; }
    public string FullDescription { get; private set; }
    public string Requirements { get; private set; }
    public string ImageUrl { get; private set; }
    public Rating Rating { get; private set; }
    public decimal Price { get; private set; }
    public decimal DiscountPrice { get; private set; }
    public TimeSpan TotalDuration { get; private set; }
    public string Language { get; private set; }
    public int Level { get; private set; }
    public bool HasHomeTask { get; private set; }
    public bool HasPossibilityToContactTheTeacher { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsUpdated { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public int CountOfStudents { get; private set; }
    public int CountOfViews { get; private set; }
    public virtual Category Category { get; private set; }
    
    public Guid CategoryId { get; private set; }
    
    public IReadOnlyCollection<Author> Authors => _authors.AsReadOnly();
    public IReadOnlyCollection<CourseModule> Modules => _modules.AsReadOnly();
    public IReadOnlyCollection<CourseComment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<Student> Students { get; private set; }
    
    public IReadOnlyCollection<Subscription> Subscriptions { get; private set; }
    
    public void AddAuthor(Author author) => _authors.Add(author);
    
    public void AddModule(CourseModule module) => _modules.Add(module);
    
    public void AddComment(CourseComment comment) => _comments.Add(comment);
    
    public void CountTotalDuration() =>_modules
        .ForEach(m => TotalDuration += m.VideoDetails.Duration);
    
    public void AddStudent() => CountOfStudents++;
    public void RemoveStudent() => CountOfStudents--;
    
    public void AddView() => CountOfViews++;
    
    private static Result<Course> ValidateCourse(Course course)
    {
        var validator = new CourseValidator();
        var result = validator.Validate(course);
        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            return Result.Failure<Course>(errors);
        }
        return Result.Success(course);
    }

    public static Result<Course> Create(
        string title, 
        string subtitle, 
        string fullDescription, 
        string requirements, 
        string imageUrl, 
        decimal price,
        decimal discountPrice, 
        string language, 
        int level,
        bool hasHomeTask,
        bool hasPossibilityToContactTheTeacher,
        Guid categoryId)
    {
        var course = new Course(
            title,
            subtitle,
            fullDescription,
            requirements,
            imageUrl,
            price,
            discountPrice,
            language,
            level,
            hasHomeTask,
            hasPossibilityToContactTheTeacher,
            categoryId);

        return ValidateCourse(course);
    }
    
    public Result Update(
        string title, 
        string subtitle, 
        string fullDescription, 
        string requirements, 
        string imageUrl, 
        Rating rating, 
        decimal price, 
        decimal discountPrice, 
        string language, 
        int level)
    {
        Title = title;
        Subtitle = subtitle;
        FullDescription = fullDescription;
        Requirements = requirements;
        ImageUrl = imageUrl;
        Rating = rating;
        Price = price;
        DiscountPrice = discountPrice;
        Language = language;
        Level = level;
        UpdatedAt = DateTime.UtcNow;
        IsUpdated = true;

        var validationResult = ValidateCourse(this);
        return validationResult.IsFailure ? validationResult : Result.Success();
    }

    public Result UpdateRating(int value)
    {
        var result = Rating.AddRate(value);
        return result.IsFailure ? 
            Result.Failure(result.Error) : 
            Result.Success();
    }
    
    public Result RemoveRate(int value)
    {
        var result = Rating.RemoveRate(value);
        return result.IsFailure ? 
            Result.Failure(result.Error) : 
            Result.Success();
    }
}