using GetUrCourse.Services.UserAPI.Core.ValueObjects;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Authors.Queries;

public record AuthorMainResponse(
    Guid Id,
    string ImageUrl,
    string FullName,
    string Bio,
    int TotalStudent,
    int TotalReviews,
    SocialLinks Links,
    double Rating)
{
    public AuthorMainResponse(
        Guid id,
        string? userImageUrl, 
        UserName userName, 
        string? userBio, 
        int totalStudents,
        int totalReviews,
        SocialLinks? links,
        double rating)
        : this(
            id,
            userImageUrl ?? string.Empty, 
            userName.ToString(), 
            userBio ?? string.Empty,
            totalStudents, 
            totalReviews, 
            links ?? SocialLinks.Empty,
            rating)
    {
    }

}
