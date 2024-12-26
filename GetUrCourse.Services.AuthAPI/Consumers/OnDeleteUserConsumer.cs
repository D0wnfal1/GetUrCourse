using System.Windows.Input;
using GetUrCourse.Contracts.User;
using GetUrCourse.Services.AuthAPI.Services;
using MassTransit;

namespace GetUrCourse.Services.AuthAPI.Consumers;

public class OnDeleteUserConsumer(AuthService authService, Logger<OnDeleteUserConsumer> logger) : IConsumer<DeleteUser>
{
    public async Task Consume(ConsumeContext<DeleteUser> context)
    {
        var message = context.Message;
        var command = new DeleteUserDTO(message.UserId);
        var (response, error)  = await authService.DeleteUserAsync(command);
        logger.LogInformation("User deleted: {UserId}", message.UserId);
    }
}