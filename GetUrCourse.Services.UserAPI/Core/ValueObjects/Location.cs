using System.Text.Json.Serialization;
using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Core.ValueObjects;

public class Location : ValueObject
{
    public const int CountryLength = 50;
    public const int CityLength = 50;

    public string? Country { get; private set; }
    public string? City { get; private set; }
    public static Location Empty =>  new (string.Empty, string.Empty);

    private Location()
    {
        
    }
    [JsonConstructor]
    private Location(string country, string city)
    {
        Country = country;
        City = city;
    }

    public static Result<Location> Create(string country, string city)
    {
        var errors = Validate(country, city);

        return errors.Count != 0 ? ValidationResult<Location>.WithErrors(errors) : Result.Success(new Location(country, city));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Country;
        yield return City;
    }

    public Result<Location> Update(string country, string city)
    {
        Country = country;
        City = city;

        return Result.Success(this);
    }

    private static List<Error> Validate(string? country, string? city)
    {
        var errors = new List<Error>();
        if (string.IsNullOrWhiteSpace(country) || country.Length > CountryLength)
            errors.Add(
                new Error(
                    nameof(country),
                    $"Country should not be empty or exceed {CountryLength} characters"));
        if (string.IsNullOrWhiteSpace(city) || city.Length > CityLength)
            errors.Add(
                new Error(
                    nameof(city),
                    $"City should not be empty or exceed {CityLength} characters"));
        return errors;
    }
}