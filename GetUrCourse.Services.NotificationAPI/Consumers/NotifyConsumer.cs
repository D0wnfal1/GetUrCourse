using GetUrCourse.Contracts.User;
using GetUrCourse.Services.NotificationAPI.Dto;
using GetUrCourse.Services.NotificationAPI.Infrastructure.NotificationService;
using MassTransit;

namespace GetUrCourse.Services.NotificationAPI.Consumers;

public class NotifyConsumer(INotificationService service): IConsumer<NotifyUser>
{
    public async Task Consume(ConsumeContext<NotifyUser> context)
    {
        var message = context.Message;
        var userDto = new UserDto()
        {
            FullName = message.FullName,
            Email = message.Email
        };
        await service.SendConfirmEmailAsync(userDto);

        Console.WriteLine($"User notified event published");
    }
}