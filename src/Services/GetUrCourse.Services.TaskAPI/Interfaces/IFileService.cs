
namespace GetUrCourse.Services.TaskAPI.Interfaces;

public interface IFileService
{
    Task<Guid> UploadFileAsync(IFormFile file);
}