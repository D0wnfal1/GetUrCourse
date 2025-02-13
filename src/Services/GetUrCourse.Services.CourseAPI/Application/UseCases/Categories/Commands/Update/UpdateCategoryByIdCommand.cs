using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Update;

public record UpdateCategoryByIdCommand(
    Guid Id,
    string? Title,
    string? Description,
    Guid? ParentCategoryId) : ICommand;
