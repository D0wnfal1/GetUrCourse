using System.Reflection.Metadata;
using GetUrCourse.Services.CourseAPI.Core.Enums;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class Subscription
{
    public const int MaxSubscriptionTitleLength = 100;
    public const int MaxSubscriptionDescriptionLength = 500;
    
    private readonly List<Course> _courses = [];
    private readonly List<Student> _students = [];
    private readonly List<StudentSubscription>_studentSubscriptions = [];

    private Subscription(string title, string description)
    {
        Title = title;
        Description = description;
    }
    
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();
    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();
    public IReadOnlyCollection<StudentSubscription> StudentSubscriptions => _studentSubscriptions.AsReadOnly();
    
    public void AddCourse(Course course) => _courses.Add(course);
    public void AddStudent(Student student) => _students.Add(student);
    
    public void RemoveCourse(Course course) => _courses.Remove(course);
    public void RemoveStudent(Student student) => _students.Remove(student);

    public static Result<Subscription> Create(string title, string description)
    {
        return Result.Success(new Subscription(title, description));
    }
    
    public Result Update(string? title, string? description)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        return Result.Success();
    }
    
    
    

}