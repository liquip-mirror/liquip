using Zarlo.Cosmos.Logger.Interfaces;

// ReSharper disable InvocationIsSkipped

namespace Zarlo.Cosmos.Logger.Sinks;

public class ConsoleSink : ISink
{
    public void Raw(string context, LogLevel logLevel, string message, string? caller, string? filePath, int? lineNumber,
        string? messageExpression)
    {
        DoRaw(context, logLevel, message, caller, filePath, lineNumber, messageExpression);
    }

    public void Dispose()
    {
    }


    private void DoRaw(string context, LogLevel logLevel, string message, string? caller, string? filePath, int? lineNumber,
        string? messageExpression)
    {
        var logLevelMessage = logLevel switch
        {
            LogLevel.Info => "Info",
            LogLevel.Error => "Error",
            LogLevel.Trace => "Trace",
            LogLevel.Debug => "Debug",
            LogLevel.Exception => "Exception",
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
        };

        var bg = Console.BackgroundColor;
        var fg = Console.ForegroundColor;

        Console.BackgroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;

        Console.Write("[");

        Console.ForegroundColor = logLevel switch
        {
            LogLevel.Info => ConsoleColor.Green,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Trace => ConsoleColor.DarkBlue,
            LogLevel.Debug => ConsoleColor.White,
            LogLevel.Exception => ConsoleColor.Magenta
        };

        Console.Write(logLevelMessage);
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write("] ");
        if (logLevel == LogLevel.Exception)
        {
            Console.Write("[{0}:{1}] \n {2}", filePath, lineNumber, message);
        }
        else
        {
            Console.Write(message);
        }
        if (!message.EndsWith(Environment.NewLine))
        {
            Console.Write(Environment.NewLine);
        }

        Console.BackgroundColor = bg;
        Console.ForegroundColor = fg;
    }
}
