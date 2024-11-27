namespace GetUrCourse.Services.NotificationAPI.Infrastructure.TemplateReader;

public class TemplateReader : ITemplateReader
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public TemplateReader(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string?> ReadTemplateAsync(string templatePath)
    {
        var pathToTemplate = Path.Combine(_webHostEnvironment.ContentRootPath, templatePath);

        if (!File.Exists(pathToTemplate))
        {
            return null;
        }

        using var sr = File.OpenText(pathToTemplate);
        return await sr.ReadToEndAsync();
    }
}