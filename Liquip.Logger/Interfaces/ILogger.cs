using System;

namespace Liquip.Logger.Interfaces;

public interface ILogger : IDisposable
{
    public void Info(
        string message
        // [CallerMemberName] string? caller = null,
        // [CallerFilePath] string? filePath = null,
        // [CallerLineNumber] int? lineNumber = 0,
        // [CallerArgumentExpression("message")]
        // string? messageExpression = null
        );

    public void Error(
        string message
        // [CallerMemberName] string? caller = null,
        // [CallerFilePath] string? filePath = null,
        // [CallerLineNumber] int? lineNumber = 0,
        // [CallerArgumentExpression("message")]
        // string? messageExpression = null
        );


    public void Exception(string message
        // [CallerMemberName] string? caller = null,
        // [CallerFilePath] string? filePath = null,
        // [CallerLineNumber] int? lineNumber = 0,
        // [CallerArgumentExpression("message")]
        // string? messageExpression = null
        );
    public void Exception(Exception exception, string message
        // [CallerMemberName] string? caller = null,
        // [CallerFilePath] string? filePath = null,
        // [CallerLineNumber] int? lineNumber = 0,
        // [CallerArgumentExpression("message")]
        // string? messageExpression = null
        );

    public void Debug(string message
        // [CallerMemberName] string? caller = null,
        // [CallerFilePath] string? filePath = null,
        // [CallerLineNumber] int? lineNumber = 0,
        // [CallerArgumentExpression("message")]
        // string? messageExpression = null
        );

    public void Trace(string message
        // [CallerMemberName] string? caller = null,
        // [CallerFilePath] string? filePath = null,
        // [CallerLineNumber] int? lineNumber = 0,
        // [CallerArgumentExpression("message")]
        // string? messageExpression = null
        );

    public void Raw(LogLevel logLevel, string message
        // [CallerMemberName] string? caller = null,
        // [CallerFilePath] string? filePath = null,
        // [CallerLineNumber] int? lineNumber = 0,
        // [CallerArgumentExpression("message")]
        // string? messageExpression = null
        );
}
