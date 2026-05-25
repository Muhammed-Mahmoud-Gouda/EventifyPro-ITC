namespace EventifyPro.DAL.Repositories.Interfaces;

/// <summary>
/// Repository interface for ScanLog entity operations.
/// Inherits from IRepository<ScanLog> and provides ScanLog-specific query operations.
/// </summary>
public interface IScanLogRepository : IGenericRepository<ScanLog>
{
    /// <summary>
    /// Gets all scan logs for a specific event, ordered by most recent first.
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of scan logs for the event.</returns>
    Task<IReadOnlyList<ScanLog>> GetScansByEventAsync(int eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all scan logs for a specific ticket.
    /// </summary>
    /// <param name="ticketId">The ticket identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of scan logs for the ticket.</returns>
    Task<IReadOnlyList<ScanLog>> GetScansByTicketAsync(int ticketId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all invalid scan logs for a specific event (scans that failed validation).
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of invalid scan logs for the event.</returns>
    Task<IReadOnlyList<ScanLog>> GetInvalidScansAsync(int eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all scan logs created within a date range for reporting and analytics.
    /// </summary>
    /// <param name="startDate">The start of the date range (inclusive).</param>
    /// <param name="endDate">The end of the date range (inclusive).</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of scan logs within the date range.</returns>
    Task<IReadOnlyList<ScanLog>> GetScansByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all scan logs performed by a specific scanner.
    /// </summary>
    /// <param name="scannedById">The scanner's user identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of scan logs performed by the scanner.</returns>
    Task<IReadOnlyList<ScanLog>> GetScansByUserAsync(string scannedById, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets scan logs where the ticket was used at a different event (wrong event scans).
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of wrong event scan logs.</returns>
    Task<IReadOnlyList<ScanLog>> GetWrongEventScansAsync(int eventId, CancellationToken cancellationToken = default);
}