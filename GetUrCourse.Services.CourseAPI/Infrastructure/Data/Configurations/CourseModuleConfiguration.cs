using GetUrCourse.Services.CourseAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.CourseAPI.Infrastructure.Data.Configurations;

public class CourseModuleConfiguration : IEntityTypeConfiguration<CourseModule>
{
    public void Configure(EntityTypeBuilder<CourseModule> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(cm => cm.Title)
            .IsRequired()
            .HasMaxLength(CourseModule.MaxTitleLength);
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(CourseModule.MaxDescriptionLength);
        builder.Property(x => x.PdfUrl);
        builder.ComplexProperty(x => x.VideoDetails, vd =>
        {
            vd.Property(x => x.VideoUrl).IsRequired();
            vd.Property(x => x.Duration).IsRequired();
        });
        builder.Property(x => x.CourseId).IsRequired();
        builder.HasOne(cm => cm.Course);
    }
}