namespace GetUrCourse.Services.NotificationAPI.Infrastructure.TemplateReader;

public interface ITemplateReader
{
    Task<string?> ReadTemplateAsync(string templatePath);
}