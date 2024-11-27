using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Commands.Create;

public class CreateManagerCommandHandler(UserDbContext context) : ICommandHandler<CreateManagerCommand>
{
    public async Task<Result> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
    {
        var manager = Manager.Create(request.Id, request.Department);
        if (manager.IsFailure)
        {
            return Result.Failure(manager.Error);
        }
        
        await context.Managers.AddAsync(manager.Value!, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}