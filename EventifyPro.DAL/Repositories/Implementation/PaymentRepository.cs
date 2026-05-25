namespace EventifyPro.DAL.Repositories.Implementation;

/// <summary>
/// Repository implementation for Payment entity operations.
/// Provides methods for querying payments by booking, status, and date range.
/// </summary>
public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public PaymentRepository(EventifyDbContext dbContext) : base(dbContext)
    {
    }

  
    public async Task<Payment?> GetPaymentByBookingAsync(
        int bookingId, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.BookingId == bookingId)
            .Include(p => p.Booking)
            .Include(p => p.Refunds)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Payment>> GetPaymentsByStatusAsync(
        PaymentStatus status, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.Status == status)
            .OrderByDescending(p => p.PaymentDate)
            .Include(p => p.Booking)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }


    public async Task<IReadOnlyList<Payment>> GetCompletedPaymentsAsync(
        DateTime fromDate, 
        DateTime toDate, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.Status == PaymentStatus.Completed 
                && p.PaymentDate >= fromDate 
                && p.PaymentDate <= toDate)
            .OrderByDescending(p => p.PaymentDate)
            .Include(p => p.Booking)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }


    public async Task<IReadOnlyList<Payment>> GetPaymentsByUserAsync(
        string userId, 
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return [];
        }

        return await _dbSet
            .Where(p => p.Booking.UserId == userId)
            .OrderByDescending(p => p.PaymentDate)
            .Include(p => p.Booking)
            .Include(p => p.Refunds)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }


    public async Task<Dictionary<PaymentStatus, decimal>> GetPaymentsTotalByStatusAsync(
        DateTime fromDate, 
        DateTime toDate, 
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.PaymentDate >= fromDate && p.PaymentDate <= toDate)
            .GroupBy(p => p.Status)
            .Select(g => new { Status = g.Key, Total = g.Sum(p => p.Amount) })
            .AsNoTracking()
            .ToDictionaryAsync(x => x.Status, x => x.Total, cancellationToken);
    }
}
