using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Authors.Commands.Create;

public record CreateAuthorCommand(
    Guid Id, 
    string FullName,  
    string ShortDescription, 
    string ImageUrl) : ICommand;
