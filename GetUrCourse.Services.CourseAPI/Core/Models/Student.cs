using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class Student
{
    public const int MaxFullNameLength = 100;
    public const int MaxImageUrlLength = 255;
    
    private readonly List<Course> _courses = new();
    private readonly List<Subscription> _subscriptions = new();
    private readonly List<StudentSubscription> _studentSubscriptions = new();
    private Student(Guid id, string fullName, string? imageUrl)
    {
        Id = id;
        FullName = fullName;
        ImageUrl = imageUrl ?? string.Empty;

    }
    
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string ImageUrl { get; private set; }
    
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

    public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.AsReadOnly();
    
    public IReadOnlyCollection<StudentSubscription> StudentSubscriptions => _studentSubscriptions.AsReadOnly();
    
    
    public void AddCourse(Course course)
    {
        _courses.Add(course);
    }

    public void RemoveCourse(Course course)
    {
        _courses.Remove(course);
    }

    public void AddSubscription(Subscription subscription)
    {
        _subscriptions.Add(subscription);
    }

    public void RemoveSubscription(Subscription subscription)
    {
        _subscriptions.Remove(subscription);
        subscription.RemoveStudent(this);
    }
    
    
    public static Result<Student> Create(Guid id, string fullName, string? imageUrl)
    {
        return Result.Success(new Student(id, fullName, imageUrl ));
    }
    
    public Result Update(string? fullName, string? imagaUrl)
    {
        FullName = fullName ?? FullName;
        ImageUrl = imagaUrl ?? ImageUrl;
        return Result.Success();
    }
    
}