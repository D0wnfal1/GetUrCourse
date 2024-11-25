using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Queries.GetBase;

public record GetBaseCategoriesQuery : IQuery<PagedList<CategoryBaseResponse>>;


public record CategoryBaseResponse(
    Guid Id,
    string Title,
    string Description,
    Guid? ParentCategoryId
);