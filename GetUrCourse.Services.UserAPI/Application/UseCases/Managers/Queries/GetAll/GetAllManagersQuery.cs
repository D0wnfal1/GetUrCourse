using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Queries.GetAll;

public record GetAllManagersQuery(
    string? SearchTerm,
    Department? Department,
    int PageNumber,
    int PageSize
    ) : IQuery<PagedList<ManagerResponse>>;
