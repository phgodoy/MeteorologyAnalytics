using MeteorologyAnalytics.Application.DTO.Response;
using MeteorologyAnalytics.Application.Interfaces;
using MeteorologyAnalytics.Domain.Interfaces;
using MeteorologyAnalytics.Domain.Pagination;

namespace MeteorologyAnalytics.Application.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _repository;

    public WeatherService(IWeatherRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedList<WeatherResponseDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var data = await _repository.GetAllAsync(pageNumber, pageSize);

        var mapped = data.Select(x => new WeatherResponseDto
        {
            Id = x.Id,
            Date = x.Date,
            TemperatureC = x.TemperatureC,
            Summary = x.Summary,
            TemperatureF = x.TemperatureF
        }).ToList();

        return new PagedList<WeatherResponseDto>(
            mapped,
            data.CurrentPage,
            data.PageSize,
            data.TotalCount,
            data.TotalPages
        );
    }

}