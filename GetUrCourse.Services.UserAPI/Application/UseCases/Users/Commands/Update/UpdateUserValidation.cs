using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Core.ValueObjects;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Update;

public class UpdateUserValidation : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidation(UserDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required")
            .IsUserExist(context);
        
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email is not valid")
            .MaximumLength(User.MaxEmailLength)
            .IsEmailUnique(context);
        
        RuleFor(x => x.Name)
            .MustBeValueObject(x => UserName.Create(x.FirstName, x.LastName))
            .When(x => x.Name is not null);

        RuleFor(x => x.Role)
            .IsRoleValid();
        
        RuleFor(x => x.Bio)
            .MaximumLength(User.MaxBioLength)
            .WithMessage("Bio is too long");
        
        RuleFor(x => x.Birthday)
            .MustBeValueObject(x => BirthdayDate.Create(x.Value))
            .When(x => x.Birthday is not null);
        
        RuleFor(x => x.Location)
            .MustBeValueObject(x => Location.Create(x.City, x.Country))
            .When(x => x.Location is not null);

        RuleFor(x => x.Sex)
            .IsSexValid()
            .When(x => x.Sex.HasValue);
        
        RuleFor(x => x.SocialLinks)
            .MustBeValueObject(x => SocialLinks.Create(
                x.FacebookLink, 
                x.TwitterLink, 
                x.LinkedInLink, 
                x.InstagramLink, 
                x.GitHubLink,
                x.WebsiteLink))
            .When(x => x.SocialLinks is not null);
        
        RuleFor(x => x.ImageUrl)
            .MaximumLength(User.MaxImageUrlLength)
            .WithMessage("Image URL is too long");
        
    }

}