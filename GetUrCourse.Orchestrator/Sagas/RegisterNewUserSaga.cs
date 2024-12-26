using GetUrCourse.Contracts.User;
using MassTransit;

namespace GetUrCourse.Orchestrator.Sagas;

public class RegisterNewUserSaga : MassTransitStateMachine<RegisterNewUserSagaData>
{
    public State AddingUser { get;  set; }
    public State NotifyingUser { get;  set; }
    
    public State Failed { get;  set; }
    public State OnComplete { get;  set; }
    
    public Event<UserAdded> UserAdded { get;  set; }
    public Event<UserNotified> UserNotified { get;  set; }
    public Event<UserAddFailed> UserAddFailed { get;  set; }
    public Event<UserNotifyFailed> UserNotifiedFailed { get;  set; }
 

    public RegisterNewUserSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => UserAdded, x => 
            x.CorrelateById(context => context.Message.UserId));

        Event(() => UserNotified, x =>
            x.CorrelateById(context => context.Message.UserId));

        Event(() => UserAddFailed, x =>
            x.CorrelateById(context => context.Message.UserId));
        
        Event(() => UserNotifiedFailed, x =>
            x.CorrelateById(context => context.Message.UserId));

        Initially(
            When(UserAdded)
                .Then(context =>
                {
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.UserAdded = true;
                })
                .TransitionTo(NotifyingUser)
                .Publish(context => new NotifyUser(
                    context.Saga.UserId,
                    context.Saga.Email,
                    "register")),
            When(UserAddFailed)
                .Then(context =>
                {
                    context.Saga.UserAdded = false;
                    context.Saga.Completed = false;
                    context.Saga.Faulted = true;
                })
                .TransitionTo(Failed)
                .Publish(context => new DeleteUser(context.Saga.UserId))
                .Publish(context => new OnFailRegistrationNewUser(context.Saga.CorrelationId) )
                .Finalize()
        );

        During(NotifyingUser,
            
            When(UserNotified)
                .Then(context =>
                {
                    context.Saga.UserNotified = true;
                    context.Saga.Completed = true;
                })
                .TransitionTo(OnComplete)
                .Publish(context => new OnCompleteRegistrationNewUser(context.Saga.CorrelationId))
                .Finalize(),
            
            When(UserNotifiedFailed)
                .Then(context =>
                {
                    context.Saga.UserNotified = false;
                    context.Saga.Completed = false;
                    context.Saga.Faulted = true;
                })
                .TransitionTo(Failed)
                .Publish(context => new DeleteUser(context.Saga.UserId))
                .Publish(context => new OnFailRegistrationNewUser(context.Saga.CorrelationId))
                .Finalize()
        );
    }
}