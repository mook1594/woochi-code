namespace WoochiCode.Core.Logger;

public class ConsoleLoggerOptions
{
    public string TimestampFormat { get; init; } = "yyyy-MM-dd HH:mm:sszzz";
    public bool IsUseColor { get; init; } = true;
    public bool IsIncludePrintLevel { get; set; } = true;
    public bool IsIncludePrintException { get; set; } = true;
    public int MaxDataStringLength { get; set; } = int.MaxValue;

    public Dictionary<LogLevel, ConsoleColor> ForegroundLogLevelColors { get; } = new()
    {
        [LogLevel.Info] = ConsoleColor.DarkGray,
        [LogLevel.Warn] = ConsoleColor.Yellow,
        [LogLevel.Error] = ConsoleColor.Red,
    };

    public Dictionary<LogCategory, ConsoleColor> ForegroundLogCategoryColors { get; } = new()
    {
        [LogCategory.Message] = ConsoleColor.DarkGray,
        [LogCategory.Tool] = ConsoleColor.Cyan,
        [LogCategory.Hook] = ConsoleColor.Magenta,
        [LogCategory.Skill] = ConsoleColor.DarkGreen,
        [LogCategory.Agent] = ConsoleColor.Blue,
    };
}
