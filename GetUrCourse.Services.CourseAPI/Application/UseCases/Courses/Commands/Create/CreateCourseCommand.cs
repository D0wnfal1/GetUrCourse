using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Enums;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.Create;

public record CreateCourseCommand(
    string Title, 
    string Subtitle, 
    string FullDescription, 
    string Requirements, 
    string ImageUrl, 
    decimal Price,
    decimal DiscountPrice,
    Language Language, 
    Level Level,
    bool HasHomeTask,
    bool HasPossibilityToContactTheTeacher,
    Guid CategoryId) : ICommand;
    
