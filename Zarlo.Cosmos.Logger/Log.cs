// ReSharper disable MemberCanBePrivate.Global

using System.Runtime.CompilerServices;
using Zarlo.Cosmos.Logger.Interfaces;
using Zarlo.Cosmos.Logger.Sinks;

namespace Zarlo.Cosmos.Logger;

public static class Log
{

    private static Dictionary<string, ILogger> loggers = new Dictionary<string, ILogger>();

    public static ISink[] DefaultSinks = new ISink[]
    {
        new ConsoleSink(),
        new CosmosDebugger()
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILogger GetLogger()
    {
        return GetLogger("global");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILogger GetLogger(string context)
    {
        if (!loggers.ContainsKey(context))
        {
            loggers.Add(context, new BaseLogger(DefaultSinks, context));
        }
        return loggers[context];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILogger GetLogger<T>()
    {
        return GetLogger(nameof(T));
    }

    public static ILogger Logger = GetLogger();
}