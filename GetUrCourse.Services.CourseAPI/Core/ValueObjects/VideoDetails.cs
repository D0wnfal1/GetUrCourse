
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.ValueObjects;

public class VideoDetails : ValueObject 
{
    public string VideoUrl { get; private set; }
    public TimeSpan Duration { get; private set; }

    public VideoDetails Empty => new( string.Empty, TimeSpan.Zero); 

    private VideoDetails(string videoUrl, TimeSpan duration) 
    {
        VideoUrl = videoUrl;
        Duration = duration;
    }

    public static Result<VideoDetails> Create(string videoUrl, TimeSpan duration)
    {
        var errors = Validate(videoUrl, duration);
        if (errors.Count != 0)
        {
            return ValidationResult<VideoDetails>.WithErrors(errors);
        }
        
        return Result.Success(new VideoDetails(videoUrl, duration));
    }
    

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return VideoUrl;
        yield return Duration;
    }
    
    public Result<VideoDetails> Update(string? videoUrl, TimeSpan? duration)
    {
        var errors = Validate(videoUrl, duration);
        if (errors.Count != 0)
        {
            return ValidationResult<VideoDetails>.WithErrors(errors);
        }
        VideoUrl = videoUrl ?? VideoUrl;
        Duration = duration ?? Duration;

        return Result.Success(this);
    }
    
    private static List<Error> Validate(string? videoUrl, TimeSpan? duration)
    {
        var errors = new List<Error>();
        if (string.IsNullOrWhiteSpace(videoUrl))
            errors.Add(
                new Error(
                    nameof(videoUrl),
                    "VideoUrl should not be empty"));
        if (duration.HasValue && duration == TimeSpan.Zero)
            errors.Add(
                new Error(
                    nameof(duration),
                    "Duration should not be empty"));
        return errors;
    }
}