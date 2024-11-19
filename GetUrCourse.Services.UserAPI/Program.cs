using FluentValidation;
using GetUrCourse.Services.UserAPI.Application.Behavior;
using GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Create;
using GetUrCourse.Services.UserAPI.Infrastructure.Caching;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<UserDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
});

services.AddStackExchangeRedisCache(opt =>
{
    var redis = configuration.GetConnectionString("Redis");
    opt.Configuration = redis;
});
services.AddScoped<ICachingService, CachingService>();

host.UseSerilog((context, configuration) =>
{
    configuration
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .WriteTo.Console();
});
    

services.AddValidatorsFromAssemblyContaining<CreateUserValidation>();
services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();