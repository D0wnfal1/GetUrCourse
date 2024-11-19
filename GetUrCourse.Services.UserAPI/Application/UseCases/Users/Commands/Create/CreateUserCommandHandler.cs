using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Create;

public class CreateUserCommandHandler(UserDbContext context) : ICommandHandler<CreateUserCommand, UserCreateResponse>
{
    public async Task<Result<UserCreateResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Name, request.Email, request.Role);

        if (user.IsFailure)
        {
            return Result.Failure<UserCreateResponse>(user.Error);
        }
        
        await context.Users.AddAsync(user.Value, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success(
            new UserCreateResponse(
                user.Value.Email, 
                user.Value.Name.FirstName, 
                user.Value.Name.LastName));
        
    }
}

public record UserCreateResponse(string Email, string FirstName, string LastName);