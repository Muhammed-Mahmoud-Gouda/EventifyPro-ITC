namespace Eventify.Domain.Enums;

/// <summary>
/// Represents the result of a QR code scan validation at event entry.
/// </summary>
/// <remarks>
/// Each scan attempt results in one of four outcomes, providing detailed validation information.
/// This enables the system to provide feedback on the scan result and log validation details
/// for security and auditing purposes.
/// </remarks>
public enum ScanResult : byte
{
    /// <summary>The ticket is valid and has not been used; entry is permitted.</summary>
    Valid = 0,
    /// <summary>The ticket has already been scanned and used; re-entry is denied.</summary>
    AlreadyUsed = 1,
    /// <summary>The QR code is invalid or could not be decoded; entry is denied.</summary>
    InvalidToken = 2,
    /// <summary>The ticket is for a different event; entry is denied for security.</summary>
    WrongEvent = 3
}