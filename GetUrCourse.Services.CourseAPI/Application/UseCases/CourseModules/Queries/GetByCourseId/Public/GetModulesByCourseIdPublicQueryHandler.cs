using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Queries.GetByCourseId.Public;

public class GetModulesByCourseIdPublicQueryHandler(CourseDbContext context) : IQueryHandler<GetModulesByCourseIdPublicQuery, PagedList<ModulePublicResponse>>
{
    public async Task<Result<PagedList<ModulePublicResponse>>> Handle(GetModulesByCourseIdPublicQuery request, CancellationToken cancellationToken)
    {
        var modulesQuery = context.CourseModules
            .AsNoTracking()
            .Where(m => m.CourseId == request.CourseId)
            .Select(m =>  new ModulePublicResponse(
                m.Id,
                m.Title,
                m.Description));
        
        var  result = await PagedList<ModulePublicResponse>
            .CreateAsync(modulesQuery, request.PageNumber, request.PageSize);

        if (result.IsFailure)
        {
            return Result.Failure<PagedList<ModulePublicResponse>>(result.Error);
        }
        
        return Result.Success(result.Value!);
    }
}