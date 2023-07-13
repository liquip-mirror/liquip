// ReSharper disable MemberCanBePrivate.Global

using System.Runtime.CompilerServices;
using Zarlo.Cosmos.Logger.Interfaces;
using Zarlo.Cosmos.Logger.Sinks;

namespace Zarlo.Cosmos.Logger;

public static class Log
{
    private static readonly Dictionary<string, ILogger> loggers = new();

    public static ISink[] DefaultSinks = { new ConsoleSink(), new CosmosDebugger() };

    public static ILogger Logger = GetLogger();

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
}
