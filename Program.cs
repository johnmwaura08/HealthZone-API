using HealthZoneAPI.Data;
using HealthZoneAPI.Services;
using HealthZoneAPI.Services.interfaces;
using HealthZoneAPI.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.WriteLine($"Here: {builder.Environment.EnvironmentName}");

if (builder.Environment.IsProduction())
{
    // railway injects the PORT environment variable
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
    builder.WebHost.UseUrls($"http://*:{port}");

}

builder.Services.AddDbContext<HealthzoneDBContext>(options =>
{
    var connStr = ConnectionHelper.GetConnectionString(builder.Configuration, builder.Environment);
    Console.WriteLine($"Connection String: {connStr}");
    options.UseNpgsql(connStr);
});

builder.Services.AddScoped<IWeightService, WeightService>();


var app = builder.Build();

var scope = app.Services.CreateScope();

await MigrationsHelper.ManageDataAsync(scope.ServiceProvider);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Healthzone API V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
