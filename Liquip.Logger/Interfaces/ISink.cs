using System;

namespace Liquip.Logger.Interfaces;

public interface ISink : IDisposable
{
    public void Raw(string context, LogLevel logLevel, string message
        // string? caller,
        // string? filePath,
        // int? lineNumber,
        // string? messageExpression
        );
}
