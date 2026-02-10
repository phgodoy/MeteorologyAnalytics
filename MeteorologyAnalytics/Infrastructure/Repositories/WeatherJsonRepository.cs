using System.Text.Json;
using MeteorologyAnalytics.Domain;
using MeteorologyAnalytics.Domain.Interfaces;
using MeteorologyAnalytics.Domain.Pagination;

namespace MeteorologyAnalytics.Infrastructure.Repositories;

public class WeatherJsonRepository : IWeatherRepository
{
    private readonly IWebHostEnvironment _environment;
    private readonly JsonSerializerOptions _jsonOptions;

    public WeatherJsonRepository(IWebHostEnvironment environment)
    {
        _environment = environment;

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    
    public async Task<PagedList<Weather>> GetAllAsync(int pageNumber, int pageSize)
    {
        var filePath = Path.Combine(
            _environment.ContentRootPath,
            "Data",
            "weather.json");

        if (!File.Exists(filePath))
            return new PagedList<Weather>(
                new List<Weather>(),
                0,
                pageNumber,
                pageSize);

        var json = await File.ReadAllTextAsync(filePath);

        var result = JsonSerializer.Deserialize<List<Weather>>(json, _jsonOptions)
                     ?? new List<Weather>();

        var count = result.Count;

        var items = result
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        Console.WriteLine($"PageSize recebido: {pageSize}");

        return new PagedList<Weather>(items, count, pageNumber, pageSize);
    }
}