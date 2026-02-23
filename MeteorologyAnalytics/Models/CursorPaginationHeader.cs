namespace MeteorologyAnalytics.Models;

public class CursorPaginationHeader
{
    public int? NextCursor { get; set; }
    public bool HasMore { get; set; }

    public CursorPaginationHeader(int? nextCursor, bool hasMore)
    {
        NextCursor = nextCursor;
        HasMore = hasMore;
    }
}