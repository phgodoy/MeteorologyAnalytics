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
    
   
    public async Task<CursorPage<Weather>> GetByKeysetAsync(int? cursor, int pageSize)
    {
        var query = _context.Weather
            .OrderBy(x => x.Id)
            .AsQueryable();

        if (cursor.HasValue)
            query = query.Where(x => x.Id > cursor.Value);

        var items = await query
            .Take(pageSize + 1)
            .ToListAsync();

        var hasMore = items.Count > pageSize;

        if (hasMore)
            items.RemoveAt(items.Count - 1);

        return new CursorPage<Weather>
        {
            Data = items,
            NextCursor = items.LastOrDefault()?.Id,
            HasMore = hasMore
        };
    }
}