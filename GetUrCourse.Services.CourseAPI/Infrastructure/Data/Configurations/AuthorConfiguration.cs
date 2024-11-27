using GetUrCourse.Services.CourseAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.CourseAPI.Infrastructure.Data.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.FullName)
            .IsRequired()
            .HasMaxLength(Author.MaxAuthorFullNameLength);
        
        builder.Property(a => a.ImageUrl)
            .HasMaxLength(Author.MaxAuthorFullNameLength);
        
        builder.HasMany(a => a.Courses)
            .WithMany(c => c.Authors)
            .UsingEntity(j => j.ToTable("CourseAuthors"));

    }
}