using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Authors.Commands.Create;

public class CreateAuthorCommandHandler(UserDbContext context) : ICommandHandler<CreateAuthorCommand>
{
    public async Task<Result> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = Author.Create(request.UserId);
        if (author.IsFailure)
        {
            return Result.Failure(author.Error);
        }
        
        await context.Authors.AddAsync(author.Value, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}