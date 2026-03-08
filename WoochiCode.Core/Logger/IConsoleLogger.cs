namespace WoochiCode.Core.Logger;

public interface IConsoleLogger
{
    void Log(LogLevel level, LogCategory category, string message, object? data = null, Exception? exception = null);
    void Info(LogCategory category, string message, object? data = null);
    void Warn(LogCategory category, string message, object? data = null);
    void Error(LogCategory category, string message, object? data = null);
    void Debug(string message, object? data = null);
}
