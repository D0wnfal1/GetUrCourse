namespace GetUrCourse.Services.CourseAPI.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        "validation_error",
        "A validation problem occurred.");

    List<Error> Errors { get; }
}