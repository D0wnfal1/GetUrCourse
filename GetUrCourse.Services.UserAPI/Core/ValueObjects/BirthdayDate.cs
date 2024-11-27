using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Core.ValueObjects;

public class BirthdayDate : ValueObject
{
    public static readonly DateOnly MinBirthDate = new DateOnly(1900, 1, 1);
    public static readonly DateOnly MaxBirthDate = new DateOnly(2021, 1, 1);
    public DateOnly Value { get; }

    public static BirthdayDate Empty => new(default);

    private BirthdayDate() { }

    [JsonConstructor]
    private BirthdayDate(DateOnly value)
    {
        Value = value;
    }

    public static Result<BirthdayDate> Create(DateOnly date)
    {
        if (date >= MaxBirthDate || date <= MinBirthDate)
        {
            return ValidationResult<BirthdayDate>.WithError(
                new Error(
                    nameof(BirthdayDate),
                    $"Birthday date should be between {MinBirthDate} and {MaxBirthDate}"));
        }

        return Result.Success(new BirthdayDate(date));
    }

    public Result<BirthdayDate> Update(DateOnly date)
    {
        if (date >= MaxBirthDate || date <= MinBirthDate)
        {
            return ValidationResult<BirthdayDate>.WithError(
                new Error(
                    nameof(BirthdayDate),
                    $"Birthday date should be between {MinBirthDate} and {MaxBirthDate}"));
        }

        return Result.Success(new BirthdayDate(date));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}