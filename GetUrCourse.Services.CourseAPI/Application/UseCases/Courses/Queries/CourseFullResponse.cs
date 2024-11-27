namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Queries;

public record CourseFullResponse(
    Guid Id,
    string Title,
    string Subtitle,
    string Description,
    string Requirements,
    string ImageUrl,
    double Rating,
    decimal Price,
    decimal? DiscountPrice,
    double TotalDuration,
    string Language,
    string Level,
    bool HasHomeTask,
    bool HasPossibilityToContactTheTeacher,
    DateTime CreatedAt,
    bool IsUpdated,
    DateTime? UpdatedAt,
    int CountOfStudents);
       
