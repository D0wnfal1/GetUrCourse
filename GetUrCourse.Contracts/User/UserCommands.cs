namespace GetUrCourse.Contracts.User;

public record AddUser(
    Guid UserId, 
    string Email, 
    string FullName, 
    string Role, 
    DateTime CreatedAt);

public record DeleteUser(Guid UserId);

public record NotifyUser(Guid Id, string Email, string FullName);
