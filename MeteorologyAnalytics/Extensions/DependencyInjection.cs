using MeteorologyAnalytics.Application.Interfaces;
using MeteorologyAnalytics.Application.Services;
using MeteorologyAnalytics.Domain.Interfaces;
using MeteorologyAnalytics.Infrastructure.Persistence;
using MeteorologyAnalytics.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MeteorologyAnalytics.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IWeatherRepository, WeatherRepository>();

        services.AddScoped<IWeatherService, WeatherService>();

        return services;
    }
}