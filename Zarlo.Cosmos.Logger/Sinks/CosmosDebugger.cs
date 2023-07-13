using System.Diagnostics;
using Zarlo.Cosmos.Logger.Interfaces;

// ReSharper disable InvocationIsSkipped

namespace Zarlo.Cosmos.Logger.Sinks;

public class CosmosDebugger : ISink
{

    public void Dispose()
    {
    }

    public void Raw(string context, LogLevel logLevel, string message, string? caller, string? filePath, int? lineNumber,
        string? messageExpression)
    {
        DoRaw(context, logLevel, message, caller, filePath, lineNumber, messageExpression);
    }

    [Conditional("COSMOSDEBUG")]
    private void DoRaw(string context, LogLevel logLevel, string message, string? caller, string? filePath, int? lineNumber,
        string? messageExpression)
    {
        // var logger = new Cosmos.Debug.Kernel.Debugger("logger", context);

        // logger.SendInternal(string.Format(message, data.ToArray()));
        // should we clean up the logger here or cache it?
    }
}
