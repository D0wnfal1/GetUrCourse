using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Queries.GetBase;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Queries.GetChildrenById;

public class GetChildrenCategoriesByIdQueryHandler(CourseDbContext context) : IQueryHandler<GetChildrenCategoriesByIdQuery, PagedList<CategoryBaseResponse>>
{
    public async Task<Result<PagedList<CategoryBaseResponse>>> Handle(GetChildrenCategoriesByIdQuery request, CancellationToken cancellationToken)
    {
        var categoryQuery = context.Categories
            .Where(c => c.ParentCategoryId == request.Id)
            .Select(c => new CategoryBaseResponse(
                c.Id,
                c.Title,
                c.Description,
                c.ParentCategoryId));
        
        var result = await PagedList<CategoryBaseResponse>
            .CreateAsync(categoryQuery, 1, 5);
        
        if (result.IsFailure)
        {
            return Result.Failure<PagedList<CategoryBaseResponse>>(
                new Error(
                "get_children_categories", 
                "Problem with getting children categories"));
        }
        
        return Result.Success(result.Value!);
    }
}