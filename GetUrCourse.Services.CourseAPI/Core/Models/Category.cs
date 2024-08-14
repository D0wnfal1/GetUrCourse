using CSharpFunctionalExtensions;
using GetUrCourse.Services.CourseAPI.Core.Validators;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class Category
{
    public const int MaxCategoryTitleLength = 100;
    public const int MaxCategoryDescriptionLength = 500;
    private readonly List<Course> _courses = [];
    private readonly List<Category> _subCategories = [];

    private Category() { }

    private Category(string title, string description, Category? parentCategory)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        ParentCategory = parentCategory;
        ParentCategoryId = parentCategory?.Id;
        CoursesCount = 0;
    }
       
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int CoursesCount { get; private set; }
    public IReadOnlyCollection<Course> Courses { get; private set; } = [];
    public Guid? ParentCategoryId { get; private set; }
    public Category? ParentCategory { get; set; }
    public IReadOnlyCollection<Category> SubCategories { get; private set; } = [];
    
    

    public void AddSubCategory(Category category) => _subCategories.Add(category);
    public void AddCourse(Course course)
    {
        _courses.Add(course);
        CoursesCount++;
    }

    public void RemoveSubCategory(Category category) => _subCategories.Remove(category);
    
    public void RemoveCourse(Course course)
    {
        _courses.Remove(course);
        CoursesCount--;
    }
    
    private static Result<Category> Validate(Category category)
    {
        var validator = new CategoryValidator();
        var result = validator.Validate(category);
        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            return Result.Failure<Category>(errors);
        }
        return Result.Success(category);
    }

    public static Result<Category> Create(
        string title, 
        string description, 
        Category? parentCategory)
    {
        var category = new Category(title, description, parentCategory);
        return Validate(category);
    }
    
    public Result Update(
        string title, 
        string description, 
        Category? parentCategory)
    {
        Title = title;
        Description = description;
        ParentCategory = parentCategory ?? ParentCategory;
        
        var validationResult = Validate(this);
        return validationResult.IsFailure ? validationResult : Result.Success();
    }
}