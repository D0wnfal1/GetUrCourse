using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Authors.Commands.Create;

public class CreateAuthorCommandHandler(CourseDbContext context) : ICommandHandler<CreateAuthorCommand>
{
    public async Task<Result> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = Author.Create(
            request.Id, 
            request.FullName, 
            request.ImageUrl);

        if (author.IsFailure)
        {
            return Result.Failure(author.Error);
        }

        await context.Authors.AddAsync(author.Value!, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}