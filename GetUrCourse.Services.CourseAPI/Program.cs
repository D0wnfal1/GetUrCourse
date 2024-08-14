using DotNetEnv;
using DotNetEnv.Configuration;
using FluentValidation;
using GetUrCourse.Services.CourseAPI;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration
    .AddDotNetEnv(".env", LoadOptions.TraversePath())
    .Build();

var secrets = await DopplerClient.FetchSecretsAsync(configuration);

services.AddDbContext<CourseDbContext>(options =>
{
    options.UseNpgsql(secrets.DbConnection);
});

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

services.AddValidatorsFromAssembly(typeof(Program).Assembly);


services.AddEndpointsApiExplorer();
services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();