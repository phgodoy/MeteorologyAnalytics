using MeteorologyAnalytics.Domain;
using MeteorologyAnalytics.Domain.Interfaces;
using MeteorologyAnalytics.Domain.Pagination;
using MeteorologyAnalytics.Infrastructure.Helpers;
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
        var query = _context.Weather.AsQueryable();

        var totalCount = await query.CountAsync();

        return await PaginationHelper.CreateAsync(
          query,
          pageNumber,
          pageSize);
    }


    public async Task<CursorPage<Weather>> GetByKeysetAsync(int? cursor, int pageSize)
    {
        var query = _context.Weather.AsQueryable();

        return await CursorPaginationHelper.CreateAsync(
            query,
            pageSize,
            x => x.Id,
            cursor
        );
    }
}