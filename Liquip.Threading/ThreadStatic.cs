using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmos.Core;
using Liquip.Threading.Core.Processing;

namespace Liquip.Threading;

/// <summary>
/// a simple thread statics
/// Note: this will leak memory
/// </summary>
/// <typeparam name="T"></typeparam>
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

/// <summary>
/// a simple Process static
/// Note: this will leak memory
/// </summary>
/// <typeparam name="T"></typeparam>
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
        _value.Add(ProcessContextManager.CurrentContext.Id, value);
    }

    public T? Value
    {
        get => _value.TryGetValue(ProcessContextManager.CurrentContext.Id, out var value) ? value : default;
        set => _value[ProcessContextManager.CurrentContext.Id] = value;
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
