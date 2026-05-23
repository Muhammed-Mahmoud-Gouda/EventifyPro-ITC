namespace EventifyPro.Domain.Enums
{
    public enum ScanResult : byte
    {
        Valid = 0,
        AlreadyUsed = 1,
        InvalidToken = 2,
        WrongEvent = 3
    }
}