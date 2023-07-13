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
        string message,
        [CallerMemberName] string? caller = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int? lineNumber = 0,
        [CallerArgumentExpression("message")]
        string? messageExpression = null
    )
    {
        throw new NotImplementedException();
    }

    public void Error(
        string message,
        [CallerMemberName] string? caller = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int? lineNumber = 0,
        [CallerArgumentExpression("message")]
        string? messageExpression = null
    )
    {
        throw new NotImplementedException();
    }

    public void Exception(string message,
        [CallerMemberName] string? caller = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int? lineNumber = 0,
        [CallerArgumentExpression("message")]
        string? messageExpression = null
    )
    {
        throw new NotImplementedException();
    }

    public void Exception(Exception exception, string message,
        [CallerMemberName] string? caller = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int? lineNumber = 0,
        [CallerArgumentExpression("message")]
        string? messageExpression = null
    )
    {
        throw new NotImplementedException();
    }

    public void Debug(string message,
        [CallerMemberName] string? caller = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int? lineNumber = 0,
        [CallerArgumentExpression("message")]
        string? messageExpression = null)
    {
        throw new NotImplementedException();
    }

    public void Trace(string message,
        [CallerMemberName] string? caller = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int? lineNumber = 0,
        [CallerArgumentExpression("message")]
        string? messageExpression = null)
    {
        throw new NotImplementedException();
    }

    public void Raw(LogLevel logLevel, string message,
        [CallerMemberName] string? caller = null,
        [CallerFilePath] string? filePath = null,
        [CallerLineNumber] int? lineNumber = 0,
        [CallerArgumentExpression("message")]
        string? messageExpression = null)
    {
        foreach (var sink in _sinks)
        {
            sink.Raw(_context ?? "", logLevel, message, caller, filePath, lineNumber, messageExpression);
        }
    }

    public void Dispose()
    {
    }
}
