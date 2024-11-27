using GetUrCourse.Services.UserAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.UserAPI.Infrastructure.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.CoursesInProgress);
        builder.Property(x => x.CoursesInProgress);
        
        builder.HasOne(a => a.User)
            .WithOne(u => u.Student)
            .HasForeignKey<Student>(a => a.UserId);

        builder.HasMany(x => x.Certificates)
            .WithOne(c => c.Student)
            .HasForeignKey(c => c.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}