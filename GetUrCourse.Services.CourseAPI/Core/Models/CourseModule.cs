using GetUrCourse.Services.CourseAPI.Core.ValueObjects;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class CourseModule
{
    public const int MaxTitleLength = 100;
    public const int MaxDescriptionLength = 500;
    public const int MaxPdfUrlLength = 255;

    private CourseModule() { }
    
    private CourseModule(
        string title, 
        VideoDetails videoDetails,
        string description,
        string pdfUrl,
        Guid courseId)
    {
        Id = Guid.NewGuid();
        Title = title;
        VideoDetails = videoDetails;
        Description = description;
        PdfUrl = pdfUrl;
        CourseId = courseId;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public VideoDetails VideoDetails { get; private set; }
    public string Description { get; private set; }
    public string PdfUrl { get; private set; }
    public Guid CourseId { get; private set; }
    public virtual Course Course { get; private set; }
    
    

    public static Result<CourseModule> Create(
        string title, 
        string videoUrl,
        TimeSpan duration,
        string description, 
        string pdfUrl,
        Guid courseId)
    {
        var videoDetailsResult = VideoDetails.Create(videoUrl, duration);
        if (videoDetailsResult.IsFailure)
        {
            return Result.Failure<CourseModule>(videoDetailsResult.Error);
        }
        var module = new CourseModule(title, videoDetailsResult.Value!, description, pdfUrl, courseId);

        return Result.Success(module);

    }

    public Result UpdateModule(
        string? title, 
        string? videoUrl,
        TimeSpan? duration,
        string? description, 
        string? pdfUrl)
    {
        var videoDetailsResult = VideoDetails.Update(videoUrl, duration);
        
        Title = title ?? Title;
        VideoDetails = videoDetailsResult.Value ?? VideoDetails;
        Description = description ?? Description;
        PdfUrl = pdfUrl ?? PdfUrl;
        
        return Result.Success();
    }
}