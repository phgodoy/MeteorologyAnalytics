using System.Linq.Expressions;
using MeteorologyAnalytics.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

public static class CursorPaginationHelper
{
    public static async Task<CursorPage<T>> CreateAsync<T>(
        IQueryable<T> query,
        int pageSize,
        Expression<Func<T, int>> keySelector,
        int? cursor)
        where T : class
    {
        if (cursor.HasValue)
        {
            query = query.Where(
                Expression.Lambda<Func<T, bool>>(
                    Expression.GreaterThan(
                        keySelector.Body,
                        Expression.Constant(cursor.Value)),
                    keySelector.Parameters));
        }

        var items = await query
            .OrderBy(keySelector)
            .Take(pageSize + 1)
            .ToListAsync();

        var hasMore = items.Count > pageSize;

        if (hasMore)
            items.RemoveAt(items.Count - 1);

        var nextCursor = items.LastOrDefault() != null
            ? (int?)keySelector.Compile()(items.Last())
            : null;

        return new CursorPage<T>
        {
            NextCursor = nextCursor,
            HasMore = hasMore
        };
    }
}