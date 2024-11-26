namespace GetUrCourse.Services.UserAPI.Core.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        "validation_error",
        "A validation problem occurred.");

    List<Error> Errors { get; }
}
