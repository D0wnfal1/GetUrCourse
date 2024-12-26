using GetUrCourse.Orchestrator.Sagas;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var rabbitConfig = builder.Configuration.GetSection("RabbitMq");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrchestratorDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumers(typeof(Program).Assembly);
    x.AddSagaStateMachine<RegisterNewUserSaga, RegisterNewUserSagaData>()
        .EntityFrameworkRepository(cfg =>
        {
            cfg.ExistingDbContext<OrchestratorDbContext>();
            cfg.UsePostgres();
        });
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/" , h =>
        {
            h.Username(rabbitConfig["Username"]!);
            h.Password(rabbitConfig["Password"]!);
        });
        
        cfg.UseInMemoryOutbox(context);
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

public class OrchestratorDbContext : DbContext
{
    public OrchestratorDbContext(DbContextOptions<OrchestratorDbContext> options) : base(options)
    {
    }

    public DbSet<RegisterNewUserSagaData> RegisterNewUserSagaData { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegisterNewUserSagaData>().HasKey(s => s.CorrelationId);
    }
}
