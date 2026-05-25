namespace EventifyPro.DAL.Repositories.Interfaces;

/// <summary>
/// Repository interface for Refund entity operations.
/// Inherits from IRepository<Refund> and provides Refund-specific query operations.
/// </summary>
public interface IRefundRepository : IGenericRepository<Refund>
{
    /// <summary>
    /// Gets all refunds associated with a specific booking.
    /// </summary>
    /// <remarks>
    /// Retrieves all refunds (pending, completed, or failed) for a given booking.
    /// Includes related payment and initiator information for complete context.
    /// </remarks>
    /// <param name="bookingId">The booking identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of refunds for the booking.</returns>
    Task<IReadOnlyList<Refund>> GetRefundsByBookingAsync(int bookingId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all refunds with a specific status.
    /// </summary>
    /// <remarks>
    /// Useful for monitoring refund progress and identifying refunds at each stage
    /// of processing (Pending, Completed, or Failed).
    /// </remarks>
    /// <param name="status">The refund status to filter by.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of refunds with the specified status.</returns>
    Task<IReadOnlyList<Refund>> GetRefundsByStatusAsync(RefundStatus status, CancellationToken cancellationToken = default);

  
    Task<IReadOnlyList<Refund>> GetPendingRefundsAsync(CancellationToken cancellationToken = default);


    Task<IReadOnlyList<Refund>> GetRefundsByInitiatorAsync(string initiatedById, CancellationToken cancellationToken = default);

   
    Task<IReadOnlyList<Refund>> GetRefundsByPaymentAsync(int paymentId, CancellationToken cancellationToken = default);

  
    Task<decimal> GetTotalRefundAmountByBookingAsync(int bookingId, CancellationToken cancellationToken = default);

  
    Task<IReadOnlyList<Refund>> GetCompletedRefundsAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
}