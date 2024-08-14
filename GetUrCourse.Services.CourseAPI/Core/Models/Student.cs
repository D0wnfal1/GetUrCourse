using CSharpFunctionalExtensions;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class Student
{
    public const int MaxFullNameLength = 100;
    private readonly List<Course> _courses = new();
    private readonly List<Subscription> _subscriptions = new();
    private Student(Guid id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }
    
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

    public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions;
    
    public void AddCourse(Course course)
    {
        _courses.Add(course);
        course.AddStudent();
    }

    public void RemoveCourse(Course course)
    {
        _courses.Remove(course);
        course.RemoveStudent();
    } 
    
    
    public static Result<Student> Create(Guid id, string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName) || fullName.Length > MaxFullNameLength)
            return Result.Failure<Student>(DomainExceptions.EmptyOrLonger(nameof(FullName), MaxFullNameLength));
        
        return Result.Success(new Student(id, fullName));
    }
    
    public Result Update(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName) || fullName.Length > MaxFullNameLength)
            return Result.Failure<Student>(DomainExceptions.EmptyOrLonger(nameof(FullName), MaxFullNameLength));
        
        FullName = fullName;
        return Result.Success();
    }
    
}