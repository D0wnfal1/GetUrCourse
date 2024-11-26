using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Core.ValueObjects;

public class UserName : ValueObject
{
    public const int MaxFirstNameLength = 50;
    public const int MaxLastNameLength = 50;
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public static Result<UserName> Create(string firstName, string lastName)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MaxFirstNameLength)
            errors.Add(new Error(nameof(firstName), "First name should not be empty"));

        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MaxLastNameLength)
            errors.Add(new Error(nameof(lastName), "Last name should not be empty"));

        if (errors.Count != 0)
            return ValidationResult<UserName>.WithErrors(errors);

        return Result.Success(new UserName
        {
            FirstName = firstName,
            LastName = lastName
        });
    }
    
    public Result<UserName> Update(UserName name)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(name.FirstName) || name.FirstName.Length > MaxFirstNameLength)
            errors.Add(new Error(nameof(name.FirstName), "First name should not be empty"));

        if (string.IsNullOrWhiteSpace(name.LastName) || name.LastName.Length > MaxLastNameLength)
            errors.Add(new Error(nameof(name.LastName), "Last name should not be empty"));

        if (errors.Count != 0)
            return ValidationResult<UserName>.WithErrors(errors);

        FirstName = name.FirstName;
        LastName = name.LastName;

        return Result.Success(this);
    }
    
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return FirstName;
        yield return LastName;
    }
}