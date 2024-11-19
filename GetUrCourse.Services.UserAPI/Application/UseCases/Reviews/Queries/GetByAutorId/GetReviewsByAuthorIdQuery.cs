using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Reviews.Queries.GetByAutorId;

public record GetReviewsByAuthorIdQuery(
    Guid AuthorId,
    string? SortColumn,
    string? SortAscending,
    int PageNumber,
    int PageSize
    ) : IQuery<PagedList<ReviewResponse>>;
