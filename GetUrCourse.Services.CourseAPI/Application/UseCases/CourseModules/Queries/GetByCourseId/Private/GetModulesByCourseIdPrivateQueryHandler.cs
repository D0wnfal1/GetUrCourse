using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Queries.GetByCourseId.Private;

public class GetModulesByCourseIdPrivateQueryHandler(CourseDbContext context) : IQueryHandler<GetModulesByCourseIdPrivateQuery, PagedList<ModulePrivateResponse>>
{
    public async Task<Result<PagedList<ModulePrivateResponse>>> Handle(
        GetModulesByCourseIdPrivateQuery request, 
        CancellationToken cancellationToken)
    {
        var modulesQueryable =  context.CourseModules
            .Where(x => x.CourseId == request.CourseId)
            .AsNoTracking()
            .Select(m => new ModulePrivateResponse(
                m.Id,
                m.Title,
                m.Description,
                m.VideoDetails.VideoUrl,
                m.VideoDetails.Duration,
                m.PdfUrl));

        var result = await PagedList<ModulePrivateResponse>
            .CreateAsync(modulesQueryable, request.PageNumber, request.PageSize);

        if (result.IsFailure)
        {
            return Result.Failure<PagedList<ModulePrivateResponse>>(result.Error);
        }
        
        return Result.Success(result.Value!);
    }
}