using MeteorologyAnalytics.Domain.Pagination;

namespace MeteorologyAnalytics.Domain.Interfaces;

public interface IWeatherRepository
{
    Task<PagedList<Weather>> GetAllAsync(int pageNumber, int pageSize);
    
    Task<CursorPage<Weather>> GetByKeysetAsync(int? cursor, int pageSize);
}