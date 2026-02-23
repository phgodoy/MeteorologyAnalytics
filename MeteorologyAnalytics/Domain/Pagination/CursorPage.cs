namespace MeteorologyAnalytics.Domain.Pagination;

public class CursorPage<T>
{
    public IReadOnlyList<T> Data { get; init; }

    public int? NextCursor { get; init; }

    public bool HasMore { get; init; }
}