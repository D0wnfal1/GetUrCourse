using CSharpFunctionalExtensions;

namespace GetUrCourse.Services.CourseAPI.Core.ValueObjects;

public class VideoDetails : ValueObject 
{
    public string VideoUrl { get; }
    public TimeSpan Duration { get; }
    private VideoDetails(string videoUrl, TimeSpan duration) 
    {
        VideoUrl = videoUrl;
        Duration = duration;
    }

    public static Result<VideoDetails> Create(string videoUrl, TimeSpan duration)
    {
        if (string.IsNullOrWhiteSpace(videoUrl))
            return Result.Failure<VideoDetails>("Video URL should not be empty");
        if (duration <= TimeSpan.Zero)
            return Result.Failure<VideoDetails>("Duration should be greater than zero");

        return Result.Success(new VideoDetails(videoUrl, duration));
    }
    

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return VideoUrl;
        yield return Duration;
    }
}