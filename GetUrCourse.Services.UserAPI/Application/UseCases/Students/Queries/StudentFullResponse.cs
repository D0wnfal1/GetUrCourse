
using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.ValueObjects;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Queries;

public record StudentFullResponse(
    Guid Id,
    string ImageUrl,
    string Name,
    string Email,
    Sex Sex, 
    BirthdayDate Birthday,
    string Bio,
    SocialLinks Links);
    
