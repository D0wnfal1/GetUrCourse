using GetUrCourse.Services.CourseAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.CourseAPI.Infrastructure.Data.Configurations;

public class CourseCommentConfiguration : IEntityTypeConfiguration<CourseComment>
{
    public void Configure(EntityTypeBuilder<CourseComment> builder)
    {
        builder.HasKey(cc => cc.Id);
        builder.Property(cc => cc.Text)
            .IsRequired()
            .HasMaxLength(CourseComment.MaxCommentLength);
        builder.Property(cc => cc.CreatedAt)
            .HasColumnType("timestamp with time zone");
        builder.Property(cc => cc.UpdatedAt)
            .HasColumnType("timestamp with time zone");
        builder.Property(cc => cc.IsUpdated);
        
        builder.HasOne(cc => cc.Course)
            .WithMany(c => c.Comments)
            .HasForeignKey(c => c.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cc => cc.Student);
    }
}