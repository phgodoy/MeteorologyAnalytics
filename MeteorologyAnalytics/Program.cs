using MeteorologyAnalytics.Application.Interfaces;
using MeteorologyAnalytics.Application.Services;
using MeteorologyAnalytics.Domain.Interfaces;
using MeteorologyAnalytics.Infrastructure.Persistence;
using MeteorologyAnalytics.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// -----------------------------
// DATABASE
// -----------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


// -----------------------------
// DEPENDENCY INJECTION
// -----------------------------

// Repository do banco
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
// Service
builder.Services.AddScoped<IWeatherService, WeatherService>();


var app = builder.Build();


// -----------------------------
// PIPELINE
// -----------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();