namespace Eventify.Domain.Constants;

/// <summary>
/// Contains default configuration values and constants used throughout the EventifyPro application.
/// </summary>
/// <remarks>
/// These constants define system-wide defaults for pagination, time limits, and background processing.
/// Values can be overridden through configuration if needed for specific deployments or scenarios.
/// </remarks>
public static class AppDefaults
{
    /// <summary>Default number of items to display per page in list views.</summary>
    public const int PageSize = 12;

    /// <summary>Expiration time in minutes for OTP (One-Time Password) codes.</summary>
    public const int OtpExpiryMinutes = 10;

    /// <summary>Number of hours within which a confirmed booking can be cancelled for full refund.</summary>
    public const int CancellationWindowHours = 24;

    /// <summary>Number of hours a waiting list entry remains valid before expiring.</summary>
    public const int WaitingListExpiryHours = 2;

    /// <summary>Maximum number of retry attempts for outbox message processing.</summary>
    public const int OutboxMaxRetries = 5;

    /// <summary>Interval in seconds between outbox message processing cycles.</summary>
    public const int OutboxIntervalSeconds = 30;

    /// <summary>Maximum length for booking reference numbers.</summary>
    public const int BookingReferenceLength = 50;
}