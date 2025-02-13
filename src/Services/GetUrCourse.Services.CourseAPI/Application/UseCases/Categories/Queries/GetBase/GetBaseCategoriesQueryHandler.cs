using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Queries.GetBase;

public class GetBaseCategoriesQueryHandler(CourseDbContext context) : IQueryHandler<GetBaseCategoriesQuery, PagedList<CategoryBaseResponse>>
{
    public async Task<Result<PagedList<CategoryBaseResponse>>> Handle(
        GetBaseCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        var category =  context.Categories
            .Where(c => c.ParentCategoryId == null)
            .Select(c => new CategoryBaseResponse(
                c.Id,
                c.Title,
                c.Description,
                c.ParentCategoryId));
        
        var result = await PagedList<CategoryBaseResponse>
            .CreateAsync(category, 1, 5);

        if (result.IsFailure)
        {
            return Result.Failure<PagedList<CategoryBaseResponse>>(
                new Error("get_base_categories", "Problem with getting base categories"));
        }
        
        return Result.Success(result.Value!);
        
    }
}