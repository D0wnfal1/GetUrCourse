using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.ValueObjects;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Update;

public record UpdateUserCommand(
    Guid Id,
    UserName? Name,
    string? Email,
    string? Bio,
    string? ImageUrl,
    Location? Location,
    Sex? Sex,
    SocialLinks? SocialLinks,
    BirthdayDate? Birthday,
    Role Role) : ICommand;


