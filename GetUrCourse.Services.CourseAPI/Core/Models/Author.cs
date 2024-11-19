using CSharpFunctionalExtensions;
using GetUrCourse.Services.CourseAPI.Core.Validators;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class Author
{
    public const int MaxAuthorFullNameLength = 50;
    public const int MaxAuthorShortDescriptionLength = 50;
    
    private readonly List<Course> _courses = [];
    private Author(Guid id, string fullName, string shortDescription, string imageUrl)
    {
        Id = id;
        FullName = fullName;
        ShortDescription = shortDescription;
        ImageUrl = imageUrl;
    }
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string ShortDescription { get; private set; }
    public string ImageUrl { get; private set; }
    
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();
    public void AddCourse(Course course) => _courses.Add(course);
    public void RemoveCourse(Course course) => _courses.Remove(course);

    private static Result<Author> Validate(Author author)
    {
        var validator = new AuthorValidator();
        var result = validator.Validate(author);
        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            return Result.Failure<Author>(errors);
        }
        return Result.Success(author);
    }
    
    
    public static Result<Author> Create(Guid id, string fullName, string shortDescription, string imageUrl)
    {
        var author = new Author(id, fullName, shortDescription, imageUrl);
        return Validate(author);
    }
    
    public Result Update(string fullName, string shortDescription, string imageUrl)
    {
        FullName = fullName;
        ImageUrl = imageUrl;
        ShortDescription = shortDescription;
        
        var validationResult = Validate(this);
        
        if (validationResult.IsFailure)
            return validationResult;

        return Result.Success();
    }
    
}