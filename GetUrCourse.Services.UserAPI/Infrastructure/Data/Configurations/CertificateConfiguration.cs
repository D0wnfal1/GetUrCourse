using System.Runtime.ConstrainedExecution;
using GetUrCourse.Services.UserAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.UserAPI.Infrastructure.Data.Configurations;

public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
{
    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(Certificate.TitleMaxLength);
        
        builder.Property(x => x.Date)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(x => x.PdfUrl)
            .IsRequired()
            .HasMaxLength(Certificate.PdfUrlMaxLength);
        
        builder.HasOne(x => x.Student)
            .WithMany(u => u.Certificates)
            .HasForeignKey(x => x.StudentId);
    }
}