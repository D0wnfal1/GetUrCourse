namespace GetUrCourse.Services.AuthAPI.ProducerMessage;

public class UserRegistrationMessage
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}