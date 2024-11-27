using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Core.ValueObjects;

namespace GetUrCourse.Services.UserAPI.Core.Models;

public class User 
{
    public const int MaxEmailLength = 50;
    public const int MaxImageUrlLength = 200;
    public const int MaxBioLength = 500;
    
    
    private User() { }
    
    private User(
        UserName name,
        string email, 
        Role role, Guid id)
    {
        Id = id;
        Email = email;
        Name = name;
        CreatedAt = DateTime.Now;
        Role = role;
    }
        
    public Guid Id { get; private set; }
    public string? ImageUrl { get; private set; }
    public UserName Name { get; private set; }
    public string Email { get; private set; }
    public BirthdayDate? Birthday { get; private set; } 
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Sex? Sex { get; private set; } = Enums.Sex.NotSpecified;
    
    public Location? Location { get; set; }
    public string? Bio { get; private set; } = string.Empty;
    public Role Role { get; private set; }
    public SocialLinks? SocialLinks { get; private set; }
    
    public virtual Author Author { get; set; }
    public virtual Student Student { get; set; }
    public virtual Manager Manager { get; set; }


    public static Result<User> Create(
        UserName name,
        string email,
        Role role, Guid id)
    {
        var user = new User(name, email, role, id);
        return Result.Success(user);
    }
    
    public Result Update(
        string? imageUrl,
        UserName? name,
        string? email,
        BirthdayDate? date,
        Location? location,
        Sex? sex,
        SocialLinks? socialLinks,
        string? bio,
        Role? role)
    {
        if (imageUrl != null)
            ImageUrl = imageUrl;

        if (name is not null && (Name.FirstName != name.FirstName || Name.LastName != name.LastName))
            Name = name;

        if (Email != email)
            Email = email;

        if (Birthday != null && !Birthday.Equals(date))
            Birthday = date;

        if (Sex != sex)
            Sex = sex;
        
        if (location is not null && (Location?.Country != location.Country || Location?.City != location.City))
            Location = location;
        
        if (socialLinks is not null)
            SocialLinks = socialLinks;
        
        if (Bio != bio)
            Bio = bio;

        if (Role != role)
            Role = role.Value;

        UpdatedAt = DateTime.Now;
        
        return Result.Success();
    }
    
    public Result AddRole(Role role)
    {
        Role = Role | role;
        UpdatedAt = DateTime.Now;
        return Result.Success();
    }
    
    public Result RemoveRole(Role role)
    {
        Role = Role & ~role;
        UpdatedAt = DateTime.Now;
        return Result.Success();
    }
}