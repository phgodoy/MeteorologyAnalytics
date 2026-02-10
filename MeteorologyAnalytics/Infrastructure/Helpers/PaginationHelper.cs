using MeteorologyAnalytics.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace MeteorologyAnalytics.Infrastructure.Helpers;

public static class PaginationHelper
{
    public static async Task<PagedList<T>> CreateAsync<T>(
        IQueryable<T> source,
        int pageNumber,
        int pageSize)
        where T : class
    {
        if (pageNumber < 1)
            pageNumber = 1;

        if (pageSize < 1)
            pageSize = 10;

        var totalCount = await source.CountAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        if (pageNumber > totalPages && totalPages > 0)
            pageNumber = totalPages;

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(
            items,
            pageNumber,
            pageSize,
            totalCount,
            totalPages);
    }
}