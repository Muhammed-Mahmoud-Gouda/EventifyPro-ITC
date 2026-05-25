namespace Eventify.Shared.Wrappers;

public class PagedResult<T>
{

    public IReadOnlyList<T> Data { get; init; } = Array.Empty<T>();
    public int TotalCount { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    public bool IsEmpty => TotalCount == 0;


    public static PagedResult<T> Create(
        IEnumerable<T> data,
        int totalCount,
        int pageNumber,
        int pageSize)
    {
        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be at least 1.");

        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be at least 1.");

        return new PagedResult<T>
        {
            Data       = data is IReadOnlyList<T> r ? r : data.ToList(),
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize   = pageSize
        };
    }

    
    public static PagedResult<T> Empty(int pageNumber = 1, int pageSize = 12)
        => Create([], 0, pageNumber, pageSize);

    public PagedResult<TOut> Map<TOut>(Func<T, TOut> mapper)
        => PagedResult<TOut>.Create(
            Data.Select(mapper).ToList(),
            TotalCount,
            PageNumber,
            PageSize);
}
