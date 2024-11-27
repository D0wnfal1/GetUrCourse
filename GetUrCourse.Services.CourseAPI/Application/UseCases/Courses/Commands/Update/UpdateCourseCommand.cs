using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Enums;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.Update;

public record UpdateCourseCommand(
    Guid Id,
    string Title, 
    string Subtitle, 
    string FullDescription, 
    string Requirements, 
    string ImageUrl, 
    decimal Price, 
    decimal DiscountPrice, 
    Language Language, 
    Level Level,
    Guid CategoryId) : ICommand;
