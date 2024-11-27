
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.ValueObjects;

public class Rating : ValueObject
{
    private Rating(double value)
    {
        Value = value;
        Count = 0;
    }

    public double Value { get; private set;}
    public int Count { get; private set; }

    public static Rating Create()
    {
        return new Rating(0);
    }
    public static Result<Rating> Create(int value)
    {
        var errors = Validate(value, 0);
        if (errors.Count != 0)
        {
            return ValidationResult<Rating>.WithErrors(errors);
        }
        
        return new Rating(value);
    }
    
    public Result<Rating> Add(int value)
    {
        var errors = Validate(value, Count);
        if (errors.Count != 0)
        {
            return ValidationResult<Rating>.WithErrors(errors);
        }
        
        Value = (Value * Count + value) / (Count + 1);
        Count++;
        return Result.Success(this);
    }

    public Result<Rating> Remove(int value)
    {
        var errors = Validate(value, Count);
        if (errors.Count != 0)
        {
            return ValidationResult<Rating>.WithErrors(errors);
        }

        Value = (Value * Count - value) / (Count - 1);
        Count--;
        return Result.Success(this);
    }
    
    public Result<Rating> Update(int oldValue, int newValue)
    {
        var errors = Validate(newValue, Count);
        if (errors.Count != 0)
        {
            return ValidationResult<Rating>.WithErrors(errors);
        }

        Value = (Value * Count - oldValue + newValue) / Count;
        return Result.Success(this);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
        yield return Count;
    }
    
    private static List<Error> Validate(double value, int count)
    {
        var errors = new List<Error>();
        if (value < 0 || value > 10)
            errors.Add(
                new Error(
                    nameof(value),
                    "Rating value should be between 0 and 10."));
        if (count < 0)
            errors.Add(
                new Error(
                    nameof(count),
                    "Rating count should not be negative."));
        return errors;
    }
}