using GetUrCourse.Services.CourseAPI.Core.Enums;
using GetUrCourse.Services.CourseAPI.Core.ValueObjects;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class Course
{
    public const int MaxCourseTitleLength = 100;
    public const int MaxCourseSubtitleLength = 200;
    public const int MaxCourseFullDescriptionLength = 2000;
    public const int MaxCourseRequirementsLength = 1000;
    public const int MaxCourseLanguageLength = 50;
    public const int MaxCourseLevelLength = 50;
    public const int MaxCourseImageUrlLength = 255;

    private readonly List<Author> _authors = [];
    private readonly List<CourseModule> _modules = [];
    private readonly List<CourseComment> _comments = [];
    private readonly List<Student> _students = [];
    
    private Course(
        string title, 
        string subtitle, 
        string fullDescription, 
        string requirements, 
        string imageUrl, 
        decimal price, 
        decimal discountPrice, 
        Language language, 
        Level level,
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
    public Language Language { get; private set; }
    public Level Level { get; private set; }
    public bool HasHomeTask { get; private set; }
    public bool HasPossibilityToContactTheTeacher { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsUpdated { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public int CountOfStudents { get; private set; }
    public int CountOfViews { get; private set; }
    public virtual Category Category { get;  set; }

    public virtual List<Subscription> Subscriptions { get; set; }

    public Guid CategoryId { get; private set; }

    public IReadOnlyCollection<Author> Authors => _authors;
    public IReadOnlyCollection<CourseModule> Modules => _modules.AsReadOnly();
    public IReadOnlyCollection<CourseComment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();
    
    // public IReadOnlyCollection<Subscription> Subscriptions { get; private set; }
    
    public void AddAuthor(Author author) => _authors.Add(author);
    
    public void AddModule(CourseModule module) => _modules.Add(module);
    
    public void AddComment(CourseComment comment) => _comments.Add(comment);
    
    public void CountTotalDuration() =>_modules
        .ForEach(m => TotalDuration += m.VideoDetails.Duration);

    public void AddStudent(Student student)
    {
        _students.Add(student);
        CountOfStudents++;
        student.AddCourse(this);
    }
    public void RemoveStudent(Student student)
    {
        _students.Remove(student);
        CountOfStudents--;
        student.RemoveCourse(this);
    }

    public void AddView() => CountOfViews++;

    public static Result<Course> Create(
        string title, 
        string subtitle, 
        string fullDescription, 
        string requirements, 
        string imageUrl, 
        decimal price,
        decimal discountPrice, 
        Language language, 
        Level level,
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

        return Result.Success(course);
    }
    
    public Result Update(
        string? title, 
        string? subtitle, 
        string? fullDescription, 
        string? requirements, 
        string? imageUrl, 
        decimal? price, 
        decimal? discountPrice, 
        Language? language, 
        Level? level,
        Guid? categoryId)
    {
        Title = title ?? Title ;
        Subtitle = subtitle ?? Subtitle;
        FullDescription = fullDescription ?? Subtitle;
        Requirements = requirements ?? Requirements;
        ImageUrl = imageUrl ?? Requirements;
        Price = price ?? Price;
        DiscountPrice = discountPrice ?? DiscountPrice;
        Language = language ?? Language ;
        Level = level ?? Level;
        CategoryId = categoryId ?? CategoryId;
        UpdatedAt = DateTime.UtcNow;
        IsUpdated = true;

        return Result.Success();
    }

    public Result AddRating(int value)
    {
        var result = Rating.Add(value);
        return result.IsFailure ? 
            Result.Failure(result.Error) : 
            Result.Success();
    }
    
    public Result RemoveRate(int value)
    {
        var result = Rating.Remove(value);
        return result.IsFailure ? 
            Result.Failure(result.Error) :
            Result.Success();
    }
}