using GetUrCourse.Services.UserAPI.Core.Enums;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Queries;

public record ManagerResponse(
    Guid Id,
    string? ImageUrl,
    string FullName,
    string Email,
    Department Department);
