using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Delete;

public record DeleteCategoryByIdCommand(Guid Id) : ICommand;
