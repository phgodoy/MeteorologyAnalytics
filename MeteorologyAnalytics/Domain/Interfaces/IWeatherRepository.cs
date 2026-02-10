using MeteorologyAnalytics.Domain.Pagination;

namespace MeteorologyAnalytics.Domain.Interfaces;

public interface IWeatherRepository
{
    Task<PagedList<Weather>> GetAllAsync(int pageNumber, int pageSize);
}