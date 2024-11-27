using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.UserAPI.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email)
            .IsUnique();
        
        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500);
        builder.ComplexProperty(c => c.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.FirstName)
                .HasColumnName("FirstName")
                .IsRequired()
                .HasMaxLength(50);
            nameBuilder.Property(n => n.LastName)
                .HasColumnName("LastName")
                .IsRequired()
                .HasMaxLength(50);
        });
        builder.Property(x => x.Email)
            .HasMaxLength(User.MaxEmailLength)
            .IsRequired();
        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamp without time zone")
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .HasColumnType("timestamp without time zone");
        
        builder.Property(x => x.Birthday)
            .HasConversion(
                v => v.Value, 
                v => BirthdayDate.Create(v).Value 
            )
            .HasColumnType("date");

        builder.Property(x => x.Sex)
            .HasConversion<int>();

        builder.OwnsOne(u => u.Location, sa =>
        {
            sa.Property(l => l.Country).HasColumnName("Country")
                .HasMaxLength(50);
            sa.Property(l => l.City).HasColumnName("City")
                .HasMaxLength(50);
        });
        
        builder.Property(x => x.Bio)
            .HasMaxLength(User.MaxBioLength);
        
        builder.OwnsOne(u => u.SocialLinks, sa =>
        {
            sa.Property(s => s.FacebookLink).HasColumnName("FacebookLink").HasMaxLength(500);
            sa.Property(s => s.TwitterLink).HasColumnName("TwitterLink").HasMaxLength(500);
            sa.Property(s => s.LinkedInLink).HasColumnName("LinkedInLink").HasMaxLength(500);
            sa.Property(s => s.InstagramLink).HasColumnName("InstagramLink").HasMaxLength(500);
            sa.Property(s => s.GitHubLink).HasColumnName("GitHubLink").HasMaxLength(500);
            sa.Property(s => s.WebsiteLink).HasColumnName("WebsiteLink").HasMaxLength(500);
        });
        
        builder.Property(x => x.Role)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne(x => x.Author)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Student)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);



    }
}