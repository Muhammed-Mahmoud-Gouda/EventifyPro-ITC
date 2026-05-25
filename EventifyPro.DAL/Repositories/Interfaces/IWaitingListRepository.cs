namespace EventifyPro.DAL.Repositories.Interfaces;

/// <summary>
/// Repository interface for WaitingList entity operations.
/// Inherits from IRepository<WaitingList> and provides WaitingList-specific query operations.
/// </summary>
public interface IWaitingListRepository : IGenericRepository<WaitingList>
{
    /// <summary>
    /// Gets all waiting list entries for a specific user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of waiting list entries for the user.</returns>
    Task<IReadOnlyList<WaitingList>> GetUserWaitingListsAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all waiting list entries for a specific ticket type.
    /// </summary>
    /// <param name="ticketTypeId">The ticket type identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of waiting list entries for the ticket type.</returns>
    Task<IReadOnlyList<WaitingList>> GetTicketTypeWaitingListAsync(int ticketTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all notified waiting list entries (users who have been notified of ticket availability).
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of notified waiting list entries.</returns>
    Task<IReadOnlyList<WaitingList>> GetNotifiedWaitingListsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all expired waiting list entries (entries that have exceeded their expiration date).
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of expired waiting list entries.</returns>
    Task<IReadOnlyList<WaitingList>> GetExpiredWaitingListsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all active (non-expired) waiting list entries for a specific event.
    /// </summary>
    /// <param name="eventId">The event identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of active waiting list entries for the event.</returns>
    Task<IReadOnlyList<WaitingList>> GetActiveWaitingListsForEventAsync(int eventId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the total quantity of tickets requested by users on the waiting list for a specific ticket type.
    /// </summary>
    /// <param name="ticketTypeId">The ticket type identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The total quantity of tickets requested.</returns>
    Task<int> GetTotalRequestedTicketsAsync(int ticketTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a user is already on the waiting list for a specific ticket type.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="ticketTypeId">The ticket type identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the user is on the waiting list, false otherwise.</returns>
    Task<bool> IsUserOnWaitingListAsync(string userId, int ticketTypeId, CancellationToken cancellationToken = default);
}