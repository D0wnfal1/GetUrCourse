
using GetUrCourse.Services.NotificationAPI.Infrastructure.MailJet;
using GetUrCourse.Services.NotificationAPI.Infrastructure.NotificationService;
using GetUrCourse.Services.NotificationAPI.Infrastructure.TemplateReader;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ITemplateReader, TemplateReader>();
builder.Services.AddTransient<INotificationService, NotificationService>();

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
