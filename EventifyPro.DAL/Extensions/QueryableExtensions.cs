namespace Eventify.DAL.Extensions;

/// <summary>
/// Extensions for IQueryable&lt;T&gt; to support efficient database-level pagination.
/// Executes COUNT and OFFSET/FETCH at the database level to avoid loading all data into memory.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Converts an IQueryable&lt;T&gt; into a PagedResult&lt;T&gt; with database-level pagination.
    /// Executes two queries: CountAsync() + Skip/Take, to avoid loading all records.
    /// </summary>
    /// <param name="query">Base query with filters already applied.</param>
    /// <param name="pageNumber">Requested page number — starts from 1.</param>
    /// <param name="pageSize">Number of items per page — default 12 (AppDefaults).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A PagedResult&lt;T&gt; containing page data and metadata.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when pageNumber or pageSize is less than 1.</exception>
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber),
                "Page number must be at least 1.");

        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize),
                "Page size must be at least 1.");

        var totalCount = await query.CountAsync(cancellationToken);

        if (totalCount == 0)
            return PagedResult<T>.Empty(pageNumber, pageSize);

        var data = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return PagedResult<T>.Create(data, totalCount, pageNumber, pageSize);
    }

    /// <summary>
    /// Applies pagination (Skip/Take) directly on IQueryable&lt;T&gt; without returning a PagedResult.
    /// Useful when you want to manually project or wrap the result later.
    /// </summary>
    public static IQueryable<T> ApplyPaging<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 12;

        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}