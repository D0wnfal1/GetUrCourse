using GetUrCourse.Services.TaskAPI.Interfaces;
using GetUrCourse.Services.TaskAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GetUrCourse.Services.TaskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;

    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        try
        {
            var fileId = await _fileService.UploadFileAsync(file);
            return Ok(new { FileId = fileId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}