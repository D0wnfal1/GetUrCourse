using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Create;

public record CreateCategoryCommand(
    string Title, 
    string Description, 
    Guid? ParentCategoryId) : ICommand;
