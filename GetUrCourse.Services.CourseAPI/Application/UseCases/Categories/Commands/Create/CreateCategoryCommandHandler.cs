using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Create;

public class CreateCategoryCommandHandler(CourseDbContext context) : ICommandHandler<CreateCategoryCommand>
{
    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(
            request.Title, 
            request.Description, 
            request.ParentCategoryId);
        
        if (category.IsFailure)
        {
            return Result.Failure(category.Error);
        }
        
        context.Categories.Add(category.Value);
        await context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}