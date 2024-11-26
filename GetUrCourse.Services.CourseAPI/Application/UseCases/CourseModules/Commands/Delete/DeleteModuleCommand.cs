using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Commands.Delete;

public record DeleteModuleCommand(Guid Id) : ICommand;
