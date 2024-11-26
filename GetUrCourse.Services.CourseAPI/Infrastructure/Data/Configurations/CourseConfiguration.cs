using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.CourseAPI.Infrastructure.Data.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(Course.MaxCourseTitleLength);
        builder.Property(x => x.Subtitle)
            .IsRequired()
            .HasMaxLength(Course.MaxCourseSubtitleLength);
        builder.Property(x => x.FullDescription)
            .IsRequired()
            .HasMaxLength(Course.MaxCourseFullDescriptionLength);
        builder.Property(x => x.Requirements)
            .IsRequired()
            .HasMaxLength(Course.MaxCourseRequirementsLength);
        builder.Property(x => x.ImageUrl);
        builder.Property(x => x.Price)
            .IsRequired();
        builder.Property(x => x.DiscountPrice)
            .IsRequired();
        builder.Property(x => x.Language)
            .IsRequired()
            .HasMaxLength(Course.MaxCourseLanguageLength);
        builder.Property(x => x.Level)
            .IsRequired()
            .HasMaxLength(Course.MaxCourseLevelLength);
        builder.Property(x => x.HasHomeTask);
        builder.Property(x => x.HasPossibilityToContactTheTeacher);
        builder.Property(x => x.CountOfStudents);
        builder.Property(x => x.CountOfViews);
        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamp with time zone");

        builder.ComplexProperty<Rating>(f => f.Rating, ratingBuilder =>
            {
                ratingBuilder.Property(r => r.Value)
                    .HasColumnName("Rating");
                ratingBuilder.Property(r => r.Count)
                    .HasColumnName("NumberOfVotes");
            });
        builder.Property(x => x.UpdatedAt)
            .HasColumnType("timestamp with time zone");
        
        builder.HasMany<Author>(c => c.Authors)
            .WithMany(a => a.Courses)
            .UsingEntity(j => j.ToTable("CourseAuthors"));
        
        builder.HasOne<Category>(c => c.Category)
            .WithMany(c => c.Courses)
            .HasForeignKey(c => c.CategoryId);
        
        builder.HasMany<CourseModule>(c => c.Modules)
            .WithOne(m => m.Course)
            .HasForeignKey(m => m.CourseId);
        
        builder.HasMany<CourseComment>(c => c.Comments)
            .WithOne(c => c.Course)
            .HasForeignKey(c => c.CourseId);
        
        builder.HasMany<Student>(c => c.Students)
            .WithMany(s => s.Courses)
            .UsingEntity(j => j.ToTable("StudentCourses"));
    }
}