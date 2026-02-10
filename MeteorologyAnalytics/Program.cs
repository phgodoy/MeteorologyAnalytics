using MeteorologyAnalytics.Application.Interfaces;
using MeteorologyAnalytics.Application.Services;
using MeteorologyAnalytics.Domain.Interfaces;
using MeteorologyAnalytics.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ✅ Adicionar suporte a Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 👉 (DI - quando você já tiver criado)
builder.Services.AddScoped<IWeatherRepository, WeatherJsonRepository>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Mapear controllers (ESSENCIAL)
app.MapControllers();

app.Run();