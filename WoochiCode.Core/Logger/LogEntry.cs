namespace WoochiCode.Core.Logger;

public record LogEntry
(
    DateTimeOffset Timestamp,
    LogLevel Level,
    LogCategory Category,
    string Message,
    object? Data,
    Exception? Exception = null
 )
{
    public string DataToString()
    {

        return "";
    }
}
