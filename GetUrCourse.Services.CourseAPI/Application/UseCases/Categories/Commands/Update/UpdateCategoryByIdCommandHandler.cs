using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Update;

public class UpdateCategoryByIdCommandHandler(CourseDbContext context) : ICommandHandler<UpdateCategoryByIdCommand>
{
    public async Task<Result> Handle(UpdateCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        var category = await context.Categories
            .Where(c => c.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            return Result.Failure(
                new Error(
                    "update_category", 
                    "Category not found"));
        }
        
        category!.Update(
            request.Title,
            request.Description,
            request.ParentCategoryId);
        
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}