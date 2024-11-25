using GetUrCourse.Services.CourseAPI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Infrastructure.Data;

public class CourseDbContext(DbContextOptions<CourseDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseComment> CourseComments { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CourseModule> CourseModules { get; set; }
    // public DbSet<RatingEntity> Ratings { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseDbContext).Assembly);
    }
}