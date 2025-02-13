using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Infrastructure.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Review> Reviews { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}