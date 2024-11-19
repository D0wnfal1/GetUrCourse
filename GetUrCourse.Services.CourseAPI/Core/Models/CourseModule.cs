using CSharpFunctionalExtensions;
using GetUrCourse.Services.CourseAPI.Core.Validators;
using GetUrCourse.Services.CourseAPI.Core.ValueObjects;

namespace GetUrCourse.Services.CourseAPI.Core.Models;

public class CourseModule
{
    public const int MaxTitleLength = 100;
    public const int MaxDescriptionLength = 500;

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
    
    private static Result<CourseModule> Validate(CourseModule module)
    {
        var validator = new CourseModuleValidator();
        var result = validator.Validate(module);
        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            return Result.Failure<CourseModule>(errors);
        }
        return Result.Success(module);
    }

    public static Result<CourseModule> AddModule(
        string title, 
        string videoUrl,
        TimeSpan duration,
        string description, 
        string pdfUrl,
        Guid courseId)
    {
        var videoDetailsResult = VideoDetails.Create(videoUrl, duration);
        if (videoDetailsResult.IsFailure)
            return Result.Failure<CourseModule>(videoDetailsResult.Error);
        
        var module = new CourseModule(title, videoDetailsResult.Value, description, pdfUrl, courseId);

        return Validate(module);

    }

    public Result UpdateModule(
        string title, 
        string videoUrl,
        TimeSpan duration,
        string description, 
        string pdfUrl)
    {
        var videoDetailsResult = VideoDetails.Create(videoUrl, duration);
        if (videoDetailsResult.IsFailure)
            return Result.Failure<CourseModule>(videoDetailsResult.Error);

        Title = title;
        VideoDetails = videoDetailsResult.Value;
        Description = description;
        PdfUrl = pdfUrl;
        
        var validationResult = Validate(this);
        return validationResult.IsFailure ? validationResult : Result.Success();
    }
}