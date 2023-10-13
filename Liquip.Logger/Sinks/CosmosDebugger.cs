using System.Diagnostics;
using Liquip.Logger.Interfaces;

// ReSharper disable InvocationIsSkipped

namespace Liquip.Logger.Sinks;

public class CosmosDebugger : ISink
{

    public void Dispose()
    {
    }

    public void Raw(string context, LogLevel logLevel, string message)
    {
        DoRaw(context, logLevel, message);
    }

    [Conditional("COSMOSDEBUG")]
    private void DoRaw(string context, LogLevel logLevel, string messagen)
    {
        // var logger = new Cosmos.Debug.Kernel.Debugger("logger", context);

        // logger.SendInternal(string.Format(message, data.ToArray()));
        // should we clean up the logger here or cache it?
    }
}
