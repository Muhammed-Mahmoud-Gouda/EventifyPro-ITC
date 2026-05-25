namespace Eventify.Domain.Enums;

/// <summary>
/// Represents the status of a payment transaction.
/// </summary>
/// <remarks>
/// Payments transition from pending state through processing to either successful completion or failure.
/// A completed payment may later be partially or fully refunded, moving to refunded state.
/// </remarks>
public enum PaymentStatus : byte
{
    /// <summary>Payment has been initiated but not yet processed by the payment gateway.</summary>
    Pending = 0,
    /// <summary>Payment has been successfully processed and funds have been received.</summary>
    Completed = 1,
    /// <summary>Payment processing failed and no funds were transferred.</summary>
    Failed = 2,
    /// <summary>Payment has been refunded to the customer.</summary>
    Refunded = 3
}