using System.Globalization;

namespace Eventify.Shared.Helpers;

/// <summary>
/// Helper utilities for working with dates and times in Eventify Pro.
///
/// Golden rule: All dates in the database are stored as UTC.
/// Conversion to local time happens only in the presentation layer (View).
/// </summary>
public static class DateTimeHelper
{
    private static readonly CultureInfo ArabicEgypt = new("ar-EG");

    /// <summary>
    /// Ensures that a DateTime is represented in UTC.
    /// If Kind=Local it is converted. If Kind=Unspecified it is treated as UTC.
    /// </summary>
    public static DateTime ToUtc(this DateTime dateTime)
    {
        return dateTime.Kind switch
        {
            DateTimeKind.Utc => dateTime,
            DateTimeKind.Local => dateTime.ToUniversalTime(),
            DateTimeKind.Unspecified => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc),
            _ => throw new ArgumentOutOfRangeException(nameof(dateTime.Kind))
        };
    }

    /// <summary>
    /// Nullable-safe version of ToUtc().
    /// </summary>
    public static DateTime? ToUtc(this DateTime? dateTime)
        => dateTime?.ToUtc();

    /// <summary>
    /// Checks whether the given date is in the future compared to UtcNow.
    /// Used in EventCreateDtoValidator for StartDate validation.
    /// </summary>
    public static bool IsInFuture(this DateTime dateTime, int bufferMinutes = 5)
        => dateTime.ToUtc() > DateTime.UtcNow.AddMinutes(bufferMinutes);

    /// <summary>
    /// Nullable-safe version of IsInFuture().
    /// </summary>
    public static bool IsInFuture(this DateTime? dateTime, int bufferMinutes = 5)
        => dateTime.HasValue && dateTime.Value.IsInFuture(bufferMinutes);

    /// <summary>
    /// Calculates the number of hours from now (UtcNow) until the given date.
    /// Returns a negative value if the date is in the past.
    ///
    /// Main usage: cancellation window validation (e.g., 24 hours before event).
    /// </summary>
    public static double HoursUntil(this DateTime dateTime)
        => (dateTime.ToUtc() - DateTime.UtcNow).TotalHours;

    /// <summary>
    /// Nullable-safe version of HoursUntil().
    /// </summary>
    public static double HoursUntil(this DateTime? dateTime)
        => dateTime.HasValue ? dateTime.Value.HoursUntil() : double.MinValue;

    /// <summary>
    /// Checks whether an event has ended (EndDate is in the past).
    /// Used in ReviewService to allow submitting reviews.
    /// </summary>
    public static bool IsOver(this DateTime endDate)
        => endDate.ToUtc() < DateTime.UtcNow;

    /// <summary>
    /// Checks whether a ticket sale window is currently open.
    /// </summary>
    public static bool IsSaleWindowOpen(DateTime? saleStart, DateTime? saleEnd)
    {
        var now = DateTime.UtcNow;

        if (saleStart.HasValue && now < saleStart.Value.ToUtc())
            return false;

        if (saleEnd.HasValue && now > saleEnd.Value.ToUtc())
            return false;

        return true;
    }

    /// <summary>
    /// Formats DateTime for UI display.
    /// Example: "Saturday 3 May 2026 — 09:30 PM"
    /// Should only be used in the Web/UI layer.
    /// </summary>
    public static string ToArabicDisplayString(this DateTime dateTime)
        => dateTime.ToString("dddd d MMMM yyyy — hh:mm tt", ArabicEgypt);
}