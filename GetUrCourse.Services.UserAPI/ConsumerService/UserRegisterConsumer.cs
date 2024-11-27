using GetUrCourse.Services.UserAPI.ConsumerMessage;
using MassTransit;

namespace GetUrCourse.Services.UserAPI.ConsumerService;

public class UserRegisterConsumer : IConsumer<UserRegistrationMessage>
{
    public Task Consume(ConsumeContext<UserRegistrationMessage> context)
    {
        var message = context.Message;
        
        Console.WriteLine(message.Id);
        Console.WriteLine(message.Name);
        Console.WriteLine(message.Email);
        return Task.CompletedTask;
    }
}