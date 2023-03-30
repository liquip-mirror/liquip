using System.Diagnostics;
using Cosmos.Zarlo.Logger.Interfaces;

// ReSharper disable InvocationIsSkipped

namespace Cosmos.Zarlo.Logger.Sinks;

public class ConsoleSink : ISink
{
    public void Raw(string context, LogLevel logLevel, string message)
    {
        Raw(context, logLevel, message, Array.Empty<object>());
    }

    public void Raw(string context, LogLevel logLevel, string message, params object[] data)
    {
        DoRaw(context, logLevel, message, data.ToArray());
    }


    void DoRaw(string context, LogLevel logLevel, string message, params object[] data)
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
        Console.Write(message, data.ToArray());
        if (!message.EndsWith(Environment.NewLine)) Console.Write(Environment.NewLine);

        Console.BackgroundColor = bg;
        Console.ForegroundColor = fg;
    }

    public void Dispose()
    {
    }
}