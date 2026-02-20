using MeteorologyAnalytics.Domain;
using MeteorologyAnalytics.Domain.Interfaces;
using MeteorologyAnalytics.Domain.Pagination;
using MeteorologyAnalytics.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeteorologyAnalytics.Infrastructure.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly AppDbContext _context;

    public WeatherRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<Weather>> GetAllAsync(int pageNumber, int pageSize)
    {
        var query = _context.Weather.AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Date)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedList<Weather>(
            items,
            pageNumber,
            pageSize,
            totalCount,
            totalPages
        );
    }
}