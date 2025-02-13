using GetUrCourse.Services.UserAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.UserAPI.Infrastructure.Data.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.TotalStudents);
        builder.Property(x => x.TotalCourses);
        builder.Property(x => x.TotalReviews);
        builder.Property(x => x.AverageRating);
        builder.HasOne(a => a.User)
            .WithOne(u => u.Author)
            .HasForeignKey<Author>(a => a.UserId);
    }
}