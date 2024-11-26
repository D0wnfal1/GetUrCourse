using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Queries.GetBase;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Queries.GetChildrenById;

public record GetChildrenCategoriesByIdQuery(Guid Id) : IQuery<PagedList<CategoryBaseResponse>>;
