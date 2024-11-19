namespace GetUrCourse.Services.TaskAPI.Models;

public class File
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public byte[] Content { get; set; }
    public DateTime UploadedAt { get; set; }
}