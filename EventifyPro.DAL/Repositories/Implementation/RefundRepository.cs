namespace EventifyPro.DAL.Repositories.Implementation;

/// <summary>
/// Repository implementation for Refund entity operations.
/// Provides methods for querying refunds by booking, status, and processing state.
/// </summary>
public class RefundRepository : GenericRepository<Refund>, IRefundRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RefundRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public RefundRepository(EventifyDbContext dbContext) : base(dbContext)
    {
    }

  
    public async Task<IReadOnlyList<Refund>> GetRefundsByBookingAsync(
        int bookingId, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.BookingId == bookingId)
            .OrderByDescending(r => r.CreatedAt)
            .Include(r => r.Payment)
            .Include(r => r.Booking)
            .Include(r => r.Initiator)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

  
    public async Task<IReadOnlyList<Refund>> GetRefundsByStatusAsync(
        RefundStatus status, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.Status == status)
            .OrderByDescending(r => r.CreatedAt)
            .Include(r => r.Payment)
            .Include(r => r.Booking)
            .Include(r => r.Initiator)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

   
    public async Task<IReadOnlyList<Refund>> GetPendingRefundsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.Status == RefundStatus.Pending)
            .OrderBy(r => r.CreatedAt)
            .Include(r => r.Payment)
            .Include(r => r.Booking)
            .Include(r => r.Initiator)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

   
    public async Task<IReadOnlyList<Refund>> GetRefundsByInitiatorAsync(
        string initiatedById, 
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(initiatedById))
        {
            return [];
        }

        return await _dbSet
            .Where(r => r.InitiatedById == initiatedById)
            .OrderByDescending(r => r.CreatedAt)
            .Include(r => r.Payment)
            .Include(r => r.Booking)
            .Include(r => r.Initiator)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }


    public async Task<IReadOnlyList<Refund>> GetRefundsByPaymentAsync(
        int paymentId, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.PaymentId == paymentId)
            .OrderByDescending(r => r.CreatedAt)
            .Include(r => r.Payment)
            .Include(r => r.Booking)
            .Include(r => r.Initiator)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

  
    public async Task<decimal> GetTotalRefundAmountByBookingAsync(
        int bookingId, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.BookingId == bookingId && r.Status == RefundStatus.Completed)
            .SumAsync(r => r.Amount, cancellationToken);
    }

    public async Task<IReadOnlyList<Refund>> GetCompletedRefundsAsync(
        DateTime fromDate, 
        DateTime toDate, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.Status == RefundStatus.Completed 
                && r.ProcessedAt >= fromDate 
                && r.ProcessedAt <= toDate)
            .OrderByDescending(r => r.ProcessedAt)
            .Include(r => r.Payment)
            .Include(r => r.Booking)
            .Include(r => r.Initiator)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
