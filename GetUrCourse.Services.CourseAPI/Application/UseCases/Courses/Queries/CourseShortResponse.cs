namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Queries;

public record CourseShortResponse(
    Guid Id, 
    string ImageUrl, 
    string Title, 
    string FullDescription, 
    double Rating, 
    decimal Price,
    double Duration, 
    string Level, 
    IEnumerable<string> Authors);
