using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<PaymentContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<LiqPayService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    string publicKey = configuration["LiqPay:PublicKey"];
    string privateKey = configuration["LiqPay:PrivateKey"];
    var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient();
    return new LiqPayService(publicKey, privateKey, httpClient);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
