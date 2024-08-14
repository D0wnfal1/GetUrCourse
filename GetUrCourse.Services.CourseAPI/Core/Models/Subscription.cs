using CSharpFunctionalExtensions;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class Subscription
{
    public const int MaxSubscriptionTitleLength = 100;
    
    private readonly List<Course> _courses = new();
    private readonly List<Student> _students = new();
    
    private Subscription(int id, string title)
    {
        Id = id;
        Title = title;
    }
    
    public int Id { get; private set; }
    public string Title { get; private set; }
    public IReadOnlyCollection<Course> Courses => _courses;
    public IReadOnlyCollection<Student> Students => _students;
    
    public void AddCourse(Course course) => _courses.Add(course);
    public void AddStudent(Student student) => _students.Add(student);
    
    public void RemoveCourse(Course course) => _courses.Remove(course);
    public void RemoveStudent(Student student) => _students.Remove(student);

    public static Result<Subscription> Create(int id, string title)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Length > MaxSubscriptionTitleLength)
            return Result.Failure<Subscription>(
                DomainExceptions.EmptyOrLonger(nameof(Title), MaxSubscriptionTitleLength));
        
        return Result.Success<Subscription>(new (id, title));
    }
    
    public Result Update(string title)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Length > MaxSubscriptionTitleLength)
            return Result.Failure<Subscription>(
                DomainExceptions.EmptyOrLonger(nameof(Title), MaxSubscriptionTitleLength));
        
        Title = title;
        return Result.Success();
    }
    
    
    

}