using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Liquip.Threading.Tasks;

public enum KernelTaskStatus
{
    Pending = 0,
    Success = 1,
    Failed = 2
}

[AsyncMethodBuilder(typeof(KernelTaskBuilder<>))]
public class KernelTask<T>
{
    private KernelTaskStatus _status;
    private T result;
    private Action continuation;

    public KernelTask(T result)
    {
        _status = KernelTaskStatus.Success;
        this.result = result;
    }

    public KernelTask(Exception exception)
    {
        _status = KernelTaskStatus.Failed;
        Exception = exception;
    }

    public KernelTask()
    {
        _status = KernelTaskStatus.Pending;
    }

    public T Result
    {
        get
        {
            switch (_status)
            {
                case KernelTaskStatus.Success: return result;
                case KernelTaskStatus.Failed:
                    //ExceptionDispatchInfo.Capture(Exception).Throw();
                    return default;
                default:
                    throw new InvalidOperationException("Fiber didn't complete");
            }
        }
    }

    public Exception Exception { get; private set; }

    public bool IsCompleted => _status != KernelTaskStatus.Pending;

    public KernelTaskAwaiter<T> GetAwaiter()
    {
        return new KernelTaskAwaiter<T>(this);
    }

    internal bool TrySetResult(T result)
    {
        if (_status != KernelTaskStatus.Pending)
        {
            return false;
        }
        else
        {
            _status = KernelTaskStatus.Success;
            this.result = result;
            continuation?.Invoke();
            return true;
        }
    }

    internal bool TrySetException(Exception exception)
    {
        if (_status != KernelTaskStatus.Pending)
        {
            return false;
        }
        else
        {
            _status = KernelTaskStatus.Failed;
            Exception = exception;
            continuation?.Invoke();
            return true;
        }
    }

    internal void RegisterContinuation(Action cont)
    {
        if (_status == KernelTaskStatus.Pending)
        {
            if (continuation is null)
            {
                continuation = cont;
            }
            else
            {
                var prev = continuation;
                continuation = () =>
                {
                    prev();
                    cont();
                };
            }
        }
        else
        {
            cont();
        }
    }
}

public readonly struct KernelTaskAwaiter<T> : INotifyCompletion
{
    private readonly KernelTask<T> task;

    public KernelTaskAwaiter(KernelTask<T> task)
    {
        this.task = task;
    }

    public bool IsCompleted => task.IsCompleted;

    public T GetResult()
    {
        return task.Result;
    }

    public void OnCompleted(Action continuation)
    {
        task.RegisterContinuation(continuation);
    }
}
