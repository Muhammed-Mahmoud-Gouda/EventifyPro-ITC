namespace Eventify.Domain.Enums;

/// <summary>
/// Represents the status of a refund request and processing.
/// </summary>
/// <remarks>
/// Refunds start as pending when initiated and transition to either completed (successful refund)
/// or failed (unsuccessful refund). Failed refunds may require manual intervention or retry.
/// </remarks>
public enum RefundStatus : byte
{
    /// <summary>The refund request has been created but not yet processed by the payment gateway.</summary>
    Pending = 0,
    /// <summary>The refund has been successfully processed and funds have been returned to the customer.</summary>
    Completed = 1,
    /// <summary>The refund processing failed and no funds were returned.</summary>
    Failed = 2
}