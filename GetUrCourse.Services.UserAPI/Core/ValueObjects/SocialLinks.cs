using System.Text.Json.Serialization;
using GetUrCourse.Services.UserAPI.Core.Shared;

namespace GetUrCourse.Services.UserAPI.Core.ValueObjects;

public class SocialLinks : ValueObject
{
    // Private parameterless constructor for deserialization
    private SocialLinks() { }

    // Private constructor for deserialization with JsonConstructor attribute
    [JsonConstructor]
    private SocialLinks(
        string? facebookLink,
        string? twitterLink,
        string? linkedInLink,
        string? instagramLink,
        string? gitHubLink,
        string? websiteLink)
    {
        FacebookLink = facebookLink;
        TwitterLink = twitterLink;
        LinkedInLink = linkedInLink;
        InstagramLink = instagramLink;
        GitHubLink = gitHubLink;
        WebsiteLink = websiteLink;
    }

    public string? FacebookLink { get; private set; } = string.Empty;
    public string? TwitterLink { get; private set; } = string.Empty;
    public string? LinkedInLink { get; private set; } = string.Empty;
    public string? InstagramLink { get; private set; } = string.Empty;
    public string? GitHubLink { get; private set; } = string.Empty;
    public string? WebsiteLink { get; private set; } = string.Empty;
    
    public static SocialLinks Empty => new ();

    public static Result<SocialLinks> Create(
        string? facebookLink,
        string? twitterLink,
        string? linkedInLink,
        string? instagramLink,
        string? gitHubLink,
        string? websiteLink)
    {
        return Result.Success(new SocialLinks(facebookLink, twitterLink, linkedInLink, instagramLink, gitHubLink, websiteLink));
    }
    
    public Result<SocialLinks> Update(
        string? facebookLink,
        string? twitterLink,
        string? linkedInLink,
        string? instagramLink,
        string? gitHubLink,
        string? websiteLink)
    {
        FacebookLink = facebookLink;
        TwitterLink = twitterLink;
        LinkedInLink = linkedInLink;
        InstagramLink = instagramLink;
        GitHubLink = gitHubLink;
        WebsiteLink = websiteLink;
        
        return Result.Success(this);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return FacebookLink;
        yield return TwitterLink;
        yield return LinkedInLink;
        yield return InstagramLink;
        yield return GitHubLink;
        yield return WebsiteLink;
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var validUri)
               && (validUri.Scheme == Uri.UriSchemeHttp || validUri.Scheme == Uri.UriSchemeHttps);
    }
}