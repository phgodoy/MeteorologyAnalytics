namespace MeteorologyAnalytics.Domain.Pagination;

public sealed class CursorPage<T>
{
    public required IReadOnlyList<T> Data { get; init; }

    public int? NextCursor { get; init; }

    public bool HasMore { get; init; }
}