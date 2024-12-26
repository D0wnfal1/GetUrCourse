using GetUrCourse.Contracts.User;
using MassTransit;

namespace GetUrCourse.Services.UserAPI;

public class RegisterUserConsumer : IConsumer<AddUser>
{
    public async Task Consume(ConsumeContext<AddUser> context)
    {
        var message = context.Message;
        Console.WriteLine($"User added:\n {message.Email},\n {message.FullName},\n {message.Role}\n");
        
        await context.Publish(
            new UserAdded(message.UserId, message.Email));
        
        Console.WriteLine($"User added event published");
    }
}