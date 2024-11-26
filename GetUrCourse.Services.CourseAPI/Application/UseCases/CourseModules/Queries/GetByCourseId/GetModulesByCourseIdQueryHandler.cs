using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Queries.GetByCourseId;

public class GetModulesByCourseIdQueryHandler(CourseDbContext context) : IQueryHandler<GetModulesByCourseIdQuery, PagedList<ModuleResponse>>
{
    public async Task<Result<PagedList<ModuleResponse>>> Handle(
        GetModulesByCourseIdQuery request, 
        CancellationToken cancellationToken)
    {
        var modules =  context.CourseModules
            .Where(x => x.CourseId == request.CourseId)
            .Select(x => new ModuleResponse(
                x.Id,
                x.Title,
                x.Description,
                x.VideoDetails.VideoUrl,
                x.VideoDetails.Duration,
                x.PdfUrl));
        
        var result = await PagedList<ModuleResponse>
            .CreateAsync(modules, request.PageNumber, request.PageSize);

        if (result.IsFailure)
        {
            return Result.Failure<PagedList<ModuleResponse>>(result.Error);
        }
        
        return Result.Success(result.Value!);
    }
}