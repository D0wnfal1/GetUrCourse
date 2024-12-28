namespace GetUrCourse.Contracts.User;


public record UserAdded(Guid UserId, string Email);
public record UserNotified(Guid UserId, string Email);

public record UserAddFailed(Guid UserId);

public record UserNotifyFailed(Guid UserId);

public record OnCompleteRegistrationNewUser(Guid CorrelationId);

public record OnFailRegistrationNewUser(Guid CorrelationId);

