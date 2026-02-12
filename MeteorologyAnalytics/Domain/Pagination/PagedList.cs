namespace MeteorologyAnalytics.Domain.Pagination;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; private set; }

    public int PageSize { get; private set; }

    public int TotalPages { get; private set; }

    public int TotalCount { get; private set; }

    public PagedList(
        IEnumerable<T> items,
        int currentPage,
        int pageSize,
        int totalCount)
    {
        TotalCount = totalCount;
        PageSize = pageSize > 100 ? 100 : pageSize;

        TotalPages = (int)Math.Ceiling((double)TotalCount / PageSize);

        CurrentPage = NormalizePage(currentPage, TotalPages);

        AddRange(items);
    }

    public PagedList(
        IEnumerable<T> items,
        int currentPage,
        int pageSize,
        int totalCount,
        int totalPages)
    {
        TotalCount = totalCount;
        PageSize = pageSize > 100 ? 100 : pageSize;

        TotalPages = totalPages;

        CurrentPage = NormalizePage(currentPage, TotalPages);

        AddRange(items);
    }

    private static int NormalizePage(int page, int totalPages)
    {
        if (page < 1)
        {
            return 1;
        }
            
        if (totalPages > 0 && page > totalPages)
        {
            return totalPages;
        }
            
        return page;
    }
}