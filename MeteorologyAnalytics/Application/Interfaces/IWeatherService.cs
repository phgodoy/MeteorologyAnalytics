using MeteorologyAnalytics.Application.DTO.Response;
using MeteorologyAnalytics.Domain.Pagination;

namespace MeteorologyAnalytics.Application.Interfaces;

public interface IWeatherService
{
    Task<PagedList<WeatherResponseDto>> GetAllAsync(int pageNumber, int pageSize);
    
    Task<CursorPage<WeatherResponseDto>> GetByCursorAsync(int? cursor, int pageSize);
}