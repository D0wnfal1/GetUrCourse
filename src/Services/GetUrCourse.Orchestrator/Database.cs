using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Orchestrator;

internal class Database
{
    public static void Migrate(WebApplication app)
    {
        using var container = app.Services.CreateScope();
        var dbContext = container.ServiceProvider.GetRequiredService<OrchestratorDbContext>();
        
        var pendingMigrations = dbContext.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
        {
            dbContext.Database.Migrate();
        }
    }
}
