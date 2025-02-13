
using GetUrCourse.Contracts.User;
using MassTransit;

namespace GetUrCourse.Orchestrator.Sagas;

public class CompleteRegisterNewUserConsumer : IConsumer<OnCompleteRegistrationNewUser>
{
    public Task Consume(ConsumeContext<OnCompleteRegistrationNewUser> context)
    {
        var message = context.Message;
        Console.WriteLine($"User registration completed: {message.CorrelationId}");
        return Task.CompletedTask;
    }
}