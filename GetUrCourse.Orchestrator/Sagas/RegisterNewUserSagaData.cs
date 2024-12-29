using MassTransit;

namespace GetUrCourse.Orchestrator.Sagas;

public class RegisterNewUserSagaData : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    
    public bool UserAdded { get; set; }
    public bool UserNotified { get; set; }
    public bool Completed { get; set; }
    public bool Faulted { get; set; }
}