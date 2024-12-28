using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Infrastructure.Data;

internal class Database
{
    public static void Migrate(WebApplication app)
    {
        using var container = app.Services.CreateScope();
        var dbContext = container.ServiceProvider.GetService<UserDbContext>();
        var pendingMigrations = dbContext!.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
        {
            dbContext.Database.Migrate();
        }
    }
}