using MeteorologyAnalytics.Application.DTO.Response;
using MeteorologyAnalytics.Application.Interfaces;
using MeteorologyAnalytics.Domain.Filters;
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
    
    public async Task<CursorPage<WeatherResponseDto>> GetByCursorAsync(int? cursor, int pageSize)
    {
        var data = await _repository.GetByKeysetAsync(cursor, pageSize);

        var mapped = data.Data
            .Select(x => new WeatherResponseDto
            {
                Id = x.Id,
                Date = x.Date,
                TemperatureC = x.TemperatureC,
                Summary = x.Summary,
                TemperatureF = x.TemperatureF
            })
            .ToList();

        return new CursorPage<WeatherResponseDto>
        {
            Data = mapped,
            NextCursor = data.NextCursor,
            HasMore = data.HasMore
        };
    }

    public async Task<PagedList<WeatherResponseDto>> GetByFilterAsync(
        int pageNumber,
        int pageSize,
        WeatherStationFilter? filter)
    {
        var data = await _repository.GetByFilterAsync(pageNumber, pageSize, filter);

        var mapped = data
            .Select(x => new WeatherResponseDto
            {
                Id = x.Id,
                Date = x.Date,
                TemperatureC = x.TemperatureC,
                Summary = x.Summary,
                TemperatureF = x.TemperatureF
            })
            .ToList();

        return new PagedList<WeatherResponseDto>(
            mapped,
            data.CurrentPage,
            data.PageSize,
            data.TotalCount,
            data.TotalPages
        );
    }

}