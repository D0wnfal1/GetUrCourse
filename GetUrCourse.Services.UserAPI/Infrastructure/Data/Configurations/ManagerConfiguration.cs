using GetUrCourse.Services.UserAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetUrCourse.Services.UserAPI.Infrastructure.Data.Configurations;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.Department)
            .HasConversion<int>();
        builder.HasOne(m => m.User)
            .WithOne(u => u.Manager)
            .HasForeignKey<Manager>(a => a.UserId);
    }
}