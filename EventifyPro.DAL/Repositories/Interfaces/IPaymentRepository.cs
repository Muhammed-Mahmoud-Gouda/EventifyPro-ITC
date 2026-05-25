namespace EventifyPro.DAL.Repositories.Interfaces;

/// <summary>
/// Repository interface for Payment entity operations.
/// Inherits from IRepository<Payment> and provides Payment-specific query operations.
/// </summary>
public interface IPaymentRepository : IGenericRepository<Payment>
{
 
    Task<Payment?> GetPaymentByBookingAsync(int bookingId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Payment>> GetPaymentsByStatusAsync(PaymentStatus status, CancellationToken cancellationToken = default);

  
    Task<IReadOnlyList<Payment>> GetCompletedPaymentsAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);

   
    Task<IReadOnlyList<Payment>> GetPaymentsByUserAsync(string userId, CancellationToken cancellationToken = default);

    
    Task<Dictionary<PaymentStatus, decimal>> GetPaymentsTotalByStatusAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
}