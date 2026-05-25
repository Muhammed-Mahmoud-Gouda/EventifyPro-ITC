namespace EventifyPro.DAL.Repositories.Interfaces;

/// <summary>
/// Repository interface for Ticket entity operations.
/// Inherits from IRepository<Ticket> and provides Ticket-specific query operations.
/// </summary>
public interface ITicketRepository : IGenericRepository<Ticket>
{
    /// <summary>
    /// Gets a ticket by its QR code.
    /// </summary>
    /// <param name="qrCode">The QR code string.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The ticket matching the QR code, or null if not found.</returns>
    Task<Ticket?> GetTicketByQRCodeAsync(string qrCode, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all tickets for a specific booking.
    /// </summary>
    /// <param name="bookingId">The booking identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of tickets for the booking.</returns>
    Task<IReadOnlyList<Ticket>> GetTicketsByBookingAsync(int bookingId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all tickets for a specific event.
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of tickets for the event.</returns>
    Task<IReadOnlyList<Ticket>> GetTicketsByEventAsync(int eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all unused (not yet scanned) tickets for a specific event.
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of unused tickets for the event.</returns>
    Task<IReadOnlyList<Ticket>> GetUnusedTicketsAsync(int eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all used (scanned) tickets for a specific event.
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of used tickets for the event.</returns>
    Task<IReadOnlyList<Ticket>> GetUsedTicketsAsync(int eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all tickets for a specific ticket type.
    /// </summary>
    /// <param name="ticketTypeId">The ticket type identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of tickets of the specified type.</returns>
    Task<IReadOnlyList<Ticket>> GetTicketsByTypeAsync(int ticketTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the count of used tickets for a specific event.
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of used tickets for the event.</returns>
    Task<int> GetUsedTicketsCountAsync(int eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the attendance rate percentage for a specific event.
    /// </summary>
    /// <remarks>
    /// Calculates (Used Tickets / Total Tickets) * 100 for the event.
    /// </remarks>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The attendance percentage (0-100), or 0 if no tickets exist.</returns>
    Task<double> GetAttendanceRateAsync(int eventId, CancellationToken cancellationToken = default);
}