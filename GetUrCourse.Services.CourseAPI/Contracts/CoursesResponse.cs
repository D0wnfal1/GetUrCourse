namespace GetUrCourse.Services.CourseAPI.Contracts;
public class CoursesResponse
{
    public Guid Id { get; }
    public string ImageUrl { get; }
    public string Title { get; }
    public string Description { get; }
    public double Rating { get; }
    public decimal Price { get; }
    public TimeSpan Duration { get; }
    public string Level { get; }
    public List<string> Authors { get; }

    public CoursesResponse(
        Guid id, 
        string imageUrl, 
        string title, 
        string description, 
        double rating, 
        decimal price, 
        TimeSpan duration, 
        string level, 
        List<string> authors)
    {
        Id = id;
        ImageUrl = imageUrl;
        Title = title;
        Description = description;
        Rating = rating;
        Price = price;
        Duration = duration;
        Level = level;
        Authors = authors;
    }
}