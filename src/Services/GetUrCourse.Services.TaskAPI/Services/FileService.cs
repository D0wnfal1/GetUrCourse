using GetUrCourse.Services.TaskAPI.Interfaces;
using GetUrCourse.Services.TaskAPI.Models;

namespace GetUrCourse.Services.TaskAPI.Services;

public class FileService : IFileService
{
    private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

    public async Task<Guid> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is invalid");

        var fileId = Guid.NewGuid();
        var filePath = Path.Combine(_basePath, fileId.ToString() + Path.GetExtension(file.FileName));

        if (!Directory.Exists(_basePath))
            Directory.CreateDirectory(_basePath);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileId;
    }
}