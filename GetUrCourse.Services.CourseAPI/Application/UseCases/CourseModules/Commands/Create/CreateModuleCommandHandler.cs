using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Commands.Create;

public class CreateModuleCommandHandler(CourseDbContext context) : ICommandHandler<CreateModuleCommand>
{
    public async Task<Result> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        var module = CourseModule.Create(
            request.Title,
            request.VideoUrl,
            request.Duration,
            request.Description,
            request.PdfUrl,
            request.CourseId);
        
        if (module.IsFailure)
            return Result.Failure(module.Error);
        
        await context.CourseModules.AddAsync(module.Value!, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}