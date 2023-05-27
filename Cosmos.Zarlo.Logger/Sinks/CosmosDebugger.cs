using System.Diagnostics;
using Cosmos.Zarlo.Logger.Interfaces;

// ReSharper disable InvocationIsSkipped

namespace Cosmos.Zarlo.Logger.Sinks;

public class CosmosDebugger : ISink
{
    public void Raw(string context, LogLevel logLevel, string message)
    {
        Raw(context, logLevel, message, Array.Empty<object>());
    }

    public void Raw(string context, LogLevel logLevel, string message, params object[] data)
    {
        DoRaw(context, logLevel, message, data.ToArray());
    }

    [Conditional("COSMOSDEBUG")]
    void DoRaw(string context, LogLevel logLevel, string message, params object[] data)
    {
        // var logger = new Cosmos.Debug.Kernel.Debugger("logger", context);

        // logger.SendInternal(string.Format(message, data.ToArray()));
        // should we clean up the logger here or cache it?
    }

    public void Dispose()
    {
    }
}