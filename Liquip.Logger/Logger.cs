using System;
using System.Runtime.CompilerServices;
using System.Text;
using Zarlo.Cosmos.Logger.Interfaces;

namespace Zarlo.Cosmos.Logger;

public class BaseLogger : ILogger
{
    private readonly string? _context;

    private readonly ISink[] _sinks;

    public BaseLogger(ISink[] sinks, string? context)
    {
        _context = context;
        _sinks = sinks;
    }

    public void Info(
        string message
    )
    {
        Raw(LogLevel.Info, message);
    }

    public void Error(
        string message
    )
    {
        Raw(LogLevel.Error, message);
    }

    public void Exception(string message
    )
    {
        Raw(LogLevel.Exception, message);
    }

    public void Exception(Exception exception, string message
    )
    {
        Raw(LogLevel.Exception, string.Format("{0} \n {1}", message, exception.ToString()));
    }

    public void Debug(string message)
    {
        Raw(LogLevel.Debug, message);
    }

    public void Trace(string message)
    {
        Raw(LogLevel.Trace, message);
    }

    public void Raw(LogLevel logLevel, string message)
    {
        foreach (var sink in _sinks)
        {
            sink.Raw(_context ?? "", logLevel, message);
        }
    }

    public void Dispose()
    {
    }
}
