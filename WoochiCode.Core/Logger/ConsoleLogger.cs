using System.Text;

namespace WoochiCode.Core.Logger;

public class ConsoleLogger : IConsoleLogger
{
    private static readonly Lazy<ConsoleLogger> _Instance = new(() => new ConsoleLogger());
    private static readonly object Sync = new();
    private readonly ConsoleLoggerOptions _Options;
    public static ConsoleLogger Instance => _Instance.Value;

    private ConsoleLogger(ConsoleLoggerOptions? options = null)
    {
        _Options = options ?? new();
    }

    public void Log(LogLevel level, LogCategory category, string message, object? data = null, Exception? exception = null)
    {
        LogEntry entry = new LogEntry(
            Timestamp: DateTimeOffset.UtcNow,
            Level: level,
            Category: category,
            Message: message,
            Data: data,
            Exception: exception);

        Write(entry);
    }

    public void Debug(string message, object? data = null)
        => Log(LogLevel.Debug, LogCategory.Message, message, data);

    public void Info(LogCategory category, string message, object? data = null)
        => Log(LogLevel.Info, category, message, data);

    public void Warn(LogCategory category, string message, object? data = null)
        => Log(LogLevel.Warn, category, message, data);

    public void Error(LogCategory category, string message, object? data = null)
        => Log(LogLevel.Error, category, message, data);

    private void Write(LogEntry entry)
    {
        string text = Format(entry);

        lock (Sync)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;

            try
            {

                if (_Options.IsUseColor && _Options.ForegroundLogCategoryColors.TryGetValue(entry.Category, out ConsoleColor CategoryColor))
                {
                    Console.ForegroundColor = CategoryColor;
                }
                if (_Options.IsUseColor && _Options.ForegroundLogLevelColors.TryGetValue(entry.Level, out ConsoleColor LevelColor))
                {
                    Console.ForegroundColor = LevelColor;
                }


                Console.WriteLine(text);
            }
            finally
            {
                Console.ForegroundColor = originalForegroundColor;
                Console.BackgroundColor = originalBackgroundColor;
                Console.ResetColor();
            }
        }
    }

    private string Format(LogEntry entry)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append('[')
            .Append(entry.Timestamp.ToString(_Options.TimestampFormat))
            .Append(']');

        if (_Options.IsIncludePrintLevel)
        {
            sb
                .Append('[')
                .Append(entry.Level.ToString().ToUpperInvariant())
                .Append(']');
        }

        sb
            .Append('[')
            .Append(entry.Category)
            .Append(']');

        sb.Append(':').Append(' ')
            .Append(entry.Message);


        if (entry.Data != null && !string.IsNullOrWhiteSpace(entry.DataToString()))
        {
            string dataMessage = entry.DataToString();
            sb.AppendLine();
            sb.Append(
                (dataMessage.Length > _Options.MaxDataStringLength) ? dataMessage.Substring(0, _Options.MaxDataStringLength - 1) : entry.Data);
        }

        if (_Options.IsIncludePrintException && entry.Exception is not null)
        {
            sb.AppendLine()
              .Append("Exception: ")
              .Append(entry.Exception.GetType().Name)
              .Append(" - ")
              .Append(entry.Exception.Message);

            if (!string.IsNullOrWhiteSpace(entry.Exception.StackTrace))
            {
                sb.AppendLine()
                  .Append(entry.Exception.StackTrace);
            }
        }

        return sb.ToString();
    }
}
