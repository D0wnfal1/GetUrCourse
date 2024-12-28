using GetUrCourse.Contracts.User;
using GetUrCourse.Services.NotificationAPI.Dto;
using GetUrCourse.Services.NotificationAPI.Infrastructure.NotificationService;
using MassTransit;

namespace GetUrCourse.Services.NotificationAPI.Consumers;

public class NotifyConsumer(INotificationService service, ILogger<NotifyConsumer> logger): IConsumer<NotifyUser>
{
    public async Task Consume(ConsumeContext<NotifyUser> context)
    {
        logger.LogInformation($"User notifying started");
        var message = context.Message;
        var userDto = new UserDto()
        {
            FullName = message.FullName,
            Email = message.Email
        };
        await service.SendConfirmEmailAsync(userDto);
        
        await context.Publish(
            new UserNotified(message.UserId, message.Email));
        
        logger.LogInformation($"User notifying finished");
    }
}