namespace MeteorologyAnalytics.Domain.Pagination;

public abstract class PagedFilter<T>
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public abstract IQueryable<T> Apply(IQueryable<T> query);
}