using System.Diagnostics;
using Zarlo.Cosmos.Logger.Interfaces;

// ReSharper disable InvocationIsSkipped

namespace Zarlo.Cosmos.Logger.Sinks;

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

    public void Dispose()
    {
    }

    [Conditional("COSMOSDEBUG")]
    private void DoRaw(string context, LogLevel logLevel, string message, params object[] data)
    {
        // var logger = new Cosmos.Debug.Kernel.Debugger("logger", context);

        // logger.SendInternal(string.Format(message, data.ToArray()));
        // should we clean up the logger here or cache it?
    }
}
