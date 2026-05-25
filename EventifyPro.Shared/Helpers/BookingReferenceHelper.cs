namespace Eventify.Shared.Helpers;

public static class BookingReferenceHelper
{
    private const string Prefix = "EVT";

    public static string Generate(
        int bookingId,
        DateTime? createdAt = null,
        int idPadWidth = 5)
    {
        if (bookingId <= 0)
            throw new ArgumentOutOfRangeException(nameof(bookingId),
                "bookingId must be greater than zero.");

        var year = (createdAt ?? DateTime.UtcNow).Year;
        var paddedId = bookingId.ToString().PadLeft(idPadWidth, '0');

        return $"{Prefix}-{year}-{paddedId}";
    }

    public static bool IsValid(string? reference)
    {
        if (string.IsNullOrWhiteSpace(reference))
            return false;
 
        var parts = reference.Split('-');

        if (parts.Length != 3)
            return false;

        if (parts[0] != Prefix)
            return false;

        if (!int.TryParse(parts[1], out var year) || year < 2000 || year > 9999)
            return false;

        if (!int.TryParse(parts[2], out var id) || id <= 0)
            return false;

        return true;
    }


    public static int? ExtractId(string reference)
    {
        if (!IsValid(reference))
            return null;

        var parts = reference.Split('-');
        return int.TryParse(parts[2], out var id) ? id : null;
    }
}
