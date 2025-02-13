using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Commands.Update;

public class UpdateModuleCommandHandler(CourseDbContext context) : ICommandHandler<UpdateModuleCommand>
{
    public async Task<Result> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
    {
        var module = await context.CourseModules.FindAsync(
            [request.Id] ,cancellationToken: cancellationToken);
        
        if (module is null)
            return ValidationResult<UpdateModuleCommand>.WithError(
                new Error("module_not_found", "Comment not found"));

        module.UpdateModule(
            request.Title,
            request.VideoUrl,
            request.Duration,
            request.Description,
            request.PdfUrl);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}