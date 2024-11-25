using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class Author
{
    public const int MaxAuthorFullNameLength = 50;
    public const int MaxAuthorImageUrlLength = 255;
    
    
    private readonly List<Course> _courses = [];
    private Author(Guid id, string fullName, string imageUrl)
    {
        Id = id;
        FullName = fullName;
        ImageUrl = imageUrl;
    }
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string ImageUrl { get; private set; }
    
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();
    public void AddCourse(Course course) => _courses.Add(course);
    public void RemoveCourse(Course course) => _courses.Remove(course);
    
    public static Result<Author> Create(Guid id, string fullName,  string imageUrl)
    {
        var author = new Author(id, fullName,  imageUrl);
        return Result.Success(author);
    }
    
    public Result Update(string fullName,  string imageUrl)
    {
        FullName = fullName;
        ImageUrl = imageUrl;
        
        return Result.Success();
    }
    
}