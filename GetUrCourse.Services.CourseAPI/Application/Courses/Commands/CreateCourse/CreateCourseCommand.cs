using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.Courses.Commands.CreateCourse;

public record CreateCourseCommand(
    string Title, 
    string Subtitle, 
    string FullDescription, 
    string Requirements, 
    string ImageUrl, 
    decimal Price,
    decimal DiscountPrice, 
    string Language, 
    int Level,
    bool HasHomeTask,
    bool HasPossibilityToContactTheTeacher,
    Guid CategoryId) : ICommand;
    
