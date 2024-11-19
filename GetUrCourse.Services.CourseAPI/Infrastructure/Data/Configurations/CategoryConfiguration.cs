using GetUrCourse.Services.CourseAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.CourseAPI.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(Category.MaxCategoryTitleLength);
        builder.Property(c => c.Description);
        builder.HasMany(c => c.Courses);
        builder.HasOne(c => c.ParentCategory)
            .WithMany(pc => pc.SubCategories)
            .IsRequired(false)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

    }
    
}