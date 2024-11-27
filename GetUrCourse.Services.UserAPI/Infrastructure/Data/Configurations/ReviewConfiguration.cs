using GetUrCourse.Services.UserAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.UserAPI.Infrastructure.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(Review.MaxTextLength);
        
        builder.Property(x => x.Rating)
            .HasMaxLength(Review.MaxRating);
        
        builder.Property(x => x.CreatedAt).IsRequired();
        
        builder.HasOne(x => x.Student)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Author)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}