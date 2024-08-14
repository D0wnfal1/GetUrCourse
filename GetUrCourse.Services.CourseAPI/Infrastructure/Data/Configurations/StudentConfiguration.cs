using GetUrCourse.Services.CourseAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.CourseAPI.Infrastructure.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(Student.MaxFullNameLength);
        builder.HasMany(u => u.Courses)
            .WithMany(c => c.Students);
        builder.HasMany<CourseComment>()
            .WithOne(cc => cc.Student)
            .HasForeignKey(cc => cc.StudentId);
    }
}