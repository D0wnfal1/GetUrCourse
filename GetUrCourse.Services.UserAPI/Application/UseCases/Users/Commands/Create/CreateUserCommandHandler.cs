using GetUrCourse.Contracts.User;
using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using MassTransit;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Create;

public class CreateUserCommandHandler(UserDbContext context, ILogger<CreateUserCommandHandler> logger) : 
    ICommandHandler<CreateUserCommand, UserCreateResponse>,
    IConsumer<AddUser>
{
    public async Task<Result<UserCreateResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Name, request.Email, request.Role, request.Id);

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
    public async Task Consume(ConsumeContext<AddUser> context)
    {
        var message = context.Message;
        var command = new CreateUserCommand(
            message.FullName, 
            message.Email, 
            Enum.Parse<Role>(message.Role), 
            message.UserId);
       
        await Handle(command, context.CancellationToken);
    }
}

public record UserCreateResponse(string Email, string FirstName, string LastName);