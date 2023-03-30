using System.Diagnostics.CodeAnalysis;
using System.Text;
using Cosmos.Zarlo.Logger.Interfaces;

namespace Cosmos.Zarlo.Logger;

public class BaseLogger : ILogger
{
    private readonly string? _context;

    private readonly ISink[] _sinks;

    public BaseLogger(ISink[] sinks, string? context)
    {
        _context = context;
        _sinks = sinks;
    }

    public void Info(string message)
    {
        Raw(LogLevel.Info, message);
    }

    public void Info(string message, params object[] data)
    {
        Raw(LogLevel.Info, message, data);
    }

    public void Error(string message)
    {
        Raw(LogLevel.Error, message);
    }

    public void Error(string message, params object[] data)
    {
        Raw(LogLevel.Error, message, data);
    }

    public void Exception(string message)
    {
        Raw(LogLevel.Exception, message);
    }

    public void Exception(string message, params object[] data)
    {
        Raw(LogLevel.Exception, message, data);
    }

    public void Exception(Exception exception, string message)
    {
        var newMessage = new StringBuilder();
        newMessage.AppendLine(message);
        newMessage.AppendLine(exception.Message);
        Raw(LogLevel.Exception, newMessage.ToString());
    }

    public void Exception(Exception exception, string message, params object[] data)
    {
        var newMessage = new StringBuilder();
        newMessage.AppendLine(message);
        newMessage.AppendLine(exception.Message);
        Raw(LogLevel.Exception, newMessage.ToString(), data);
    }

    public void Debug(string message)
    {
        Raw(LogLevel.Debug, message);
    }

    public void Debug(string message, params object[] data)
    {
        Raw(LogLevel.Debug, message, data);
    }

    public void Trace(string message)
    {
        Raw(LogLevel.Trace, message);
    }

    public void Trace(string message, params object[] data)
    {
        Raw(LogLevel.Trace, message, data);
    }

    public void Raw(LogLevel logLevel, string message)
    {
        foreach (var sink in _sinks)
        {
            sink.Raw(_context ?? "", logLevel, message);
        }
    }

    public void Raw(LogLevel logLevel, string message, params object[] data)
    {
        foreach (var sink in _sinks)
        {
            sink.Raw(_context ?? "", logLevel, message, data);
        }
    }

    public void Dispose()
    {
    }
}