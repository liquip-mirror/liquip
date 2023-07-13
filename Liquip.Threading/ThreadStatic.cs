using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmos.Core;
using Liquip.Threading.Core.Processing;

namespace Liquip.Threading;

public sealed class ThreadStatic<T> : IDisposable
{
    private readonly Dictionary<uint, T?> _value;

    public ThreadStatic()
    {
        _value = new Dictionary<uint, T?>();
    }

    public ThreadStatic(T value)
    {
        _value = new Dictionary<uint, T?>();
        _value.Add(Thread.Current.ThreadID, value);
    }

    public T? Value
    {
        get => _value.TryGetValue(Thread.Current.ThreadID, out var value) ? value : default;
        set => _value[Thread.Current.ThreadID] = value;
    }

    public void Dispose()
    {
        foreach (var item in _value)
        {
            GCImplementation.Free(item.Value);
        }

        GCImplementation.Free(_value);
    }

    public static explicit operator T?(ThreadStatic<T> value)
    {
        return value.Value;
    }
}

public sealed class ProcessStatic<T> : IDisposable
{
    private readonly Dictionary<uint, T?> _value;

    public ProcessStatic()
    {
        _value = new Dictionary<uint, T?>();
    }

    public ProcessStatic(T value)
    {
        _value = new Dictionary<uint, T?>();
        _value.Add(ProcessContextManager.m_CurrentContext.tid, value);
    }

    public T? Value
    {
        get => _value.TryGetValue(ProcessContextManager.m_CurrentContext.tid, out var value) ? value : default;
        set => _value[ProcessContextManager.m_CurrentContext.tid] = value;
    }

    public void Dispose()
    {
        foreach (var item in _value)
        {
            GCImplementation.Free(item.Value);
        }

        GCImplementation.Free(_value);
    }

    public static explicit operator T?(ProcessStatic<T> value)
    {
        return value.Value;
    }
}
