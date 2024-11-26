using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class Category
{
    public const int MaxCategoryTitleLength = 100;
    public const int MaxCategoryDescriptionLength = 500;
    private readonly List<Course> _courses = [];
    private readonly List<Category> _subCategories = [];

    private Category() { }

    private Category(string title, string description, Guid? parentCategory)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        ParentCategoryId = parentCategory ?? Guid.Empty;
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

    public static Result<Category> Create(
        string title, 
        string description, 
        Guid? parentCategory)
    {
        var category = new Category(title, description, parentCategory);
        return Result.Success(category);
    }
    
    public Result Update(
        string? title, 
        string? description, 
        Guid? parentCategoryId)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        ParentCategoryId = parentCategoryId ?? ParentCategoryId;
        
        return Result.Success();
    }
}