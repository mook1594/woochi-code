namespace WoochiCode.Core.Logger;

using System.Text.Json;

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
        if (Data is null)
            return "";

        if (Data is string str)
            return str;

        return JsonSerializer.Serialize(Data, AppConstracts.JsonOpts);
    }
}
