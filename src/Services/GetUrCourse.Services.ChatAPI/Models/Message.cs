namespace GetUrCourse.Services.ChatAPI.Models;

public class Message
{
    public string User { get; set; }
    public string Group { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
}