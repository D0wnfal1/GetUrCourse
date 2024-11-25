using GetUrCourse.Services.CourseAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.CourseAPI.Infrastructure.Data.Configurations;

public class StudentSubscriptionConfiguration : IEntityTypeConfiguration<StudentSubscription>
{
    public void Configure(EntityTypeBuilder<StudentSubscription> builder)
    {
        builder.HasKey(ss => new { ss.StudentId, ss.SubscriptionId });

        builder.Property(ss => ss.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(ss => ss.StartDate)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(ss => ss.EndDate)
            .HasColumnType("timestamp with time zone");

        builder.Property(ss => ss.RemainingTime)
            .HasColumnType("interval");
        
       
        builder.HasOne(ss => ss.Student)
            .WithMany(s => s.StudentSubscriptions)
            .HasForeignKey(ss => ss.StudentId);

        builder.HasOne(ss => ss.Subscription)
            .WithMany(s => s.StudentSubscriptions)
            .HasForeignKey(ss => ss.SubscriptionId);

    }
}