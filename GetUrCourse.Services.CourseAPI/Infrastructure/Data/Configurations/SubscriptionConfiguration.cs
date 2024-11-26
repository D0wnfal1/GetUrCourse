using GetUrCourse.Services.CourseAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.CourseAPI.Infrastructure.Data.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(Subscription.MaxSubscriptionTitleLength);
        
        builder.HasMany(s => s.Courses)
            .WithMany(c => c.Subscriptions)
            .UsingEntity(j => j.ToTable("CourseSubscriptions"));
        
        builder.HasMany(s => s.Students)
            .WithMany(s => s.Subscriptions)
            .UsingEntity<StudentSubscription>();
    }
}