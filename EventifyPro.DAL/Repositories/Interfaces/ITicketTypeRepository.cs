namespace EventifyPro.DAL.Repositories.Interfaces;

/// <summary>
/// Repository interface for TicketType entity operations.
/// Inherits from IRepository<TicketType> and provides TicketType-specific query operations.
/// </summary>
public interface ITicketTypeRepository : IGenericRepository<TicketType>
{
    /// <summary>
    /// Gets all ticket types for a specific event.
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of ticket types for the event.</returns>
    Task<IReadOnlyList<TicketType>> GetTicketTypesByEventAsync(int eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets available ticket types (not sold out) for a specific event.
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of available ticket types.</returns>
    Task<IReadOnlyList<TicketType>> GetAvailableTicketTypesAsync(int eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets sold out ticket types for a specific event.
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of sold out ticket types.</returns>
    Task<IReadOnlyList<TicketType>> GetSoldOutTicketTypesAsync(int eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets ticket types with active sales period for a specific event.
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of ticket types currently on sale.</returns>
    Task<IReadOnlyList<TicketType>> GetActiveTicketTypesAsync(int eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the remaining available quantity for a specific ticket type.
    /// </summary>
    /// <param name="ticketTypeId">The ticket type identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of available tickets remaining.</returns>
    Task<int> GetAvailableQuantityAsync(int ticketTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a ticket type is currently on sale (within sale date range).
    /// </summary>
    /// <param name="ticketTypeId">The ticket type identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the ticket type is on sale, false otherwise.</returns>
    Task<bool> IsOnSaleAsync(int ticketTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the cheapest ticket type for a specific event.
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The ticket type with the lowest price, or null if no types exist.</returns>
    Task<TicketType?> GetCheapestTicketTypeAsync(int eventId, CancellationToken cancellationToken = default);
}