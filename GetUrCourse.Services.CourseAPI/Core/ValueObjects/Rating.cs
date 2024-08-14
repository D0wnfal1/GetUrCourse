using CSharpFunctionalExtensions;

namespace GetUrCourse.Services.CourseAPI.Core.ValueObjects;

public class Rating : ValueObject
{
    private Rating(double value, int count)
    {
        Value = value;
        Count = count;
    }

    public double Value { get; private set;}
    public int Count { get; private set; }

    public static Rating Create()
    {
        return new Rating(0,0);
    }

    public Result<Rating> AddRate(int value)
    {
        if (value < 0 || value > 10)
            return Result.Failure<Rating>("Rating value should be between 0 and 10.");
        
        Value = (Value * Count + value) / (Count + 1);
        Count++;
        return Result.Success(this);
    }

    public Result<Rating> RemoveRate(int value)
    {
        if (value < 0 || value > 10)
            return Result.Failure<Rating>("Rating value should be between 0 and 10.");
        if (Count <= 1)
            return Result.Failure<Rating>("Cannot remove rate from empty rating.");

        Value = (Value * Count - value) / (Count - 1);
        Count--;
        return Result.Success(this);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
        yield return Count;
    }
}