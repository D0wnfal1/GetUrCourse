using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.ValueObjects;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Queries;

public record UserFullResponse(
    Guid Id,
    string ImageUrl,
    UserName Name,
    string Email,
    BirthdayDate Birthday,
    Sex Sex,
    Location Location,
    string Bio,
    SocialLinks SocialLinks);
