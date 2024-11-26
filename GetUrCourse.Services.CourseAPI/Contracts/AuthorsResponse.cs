namespace GetUrCourse.Services.CourseAPI.Contracts;

public class AuthorsResponse
{
    public Guid Id { get; init; }
    public string ImageUrl { get; init; }
    public string FullName { get; set; }
    public string ShortDecription { get; set; }
}