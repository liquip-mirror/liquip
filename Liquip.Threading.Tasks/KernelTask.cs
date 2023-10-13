using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Liquip.Threading.Tasks;

[AsyncMethodBuilder(typeof(KernelTaskBuilder))]
public class KernelTask
{
    private KernelTaskStatus _status;
    private Action _continuation;

    public KernelTask(Exception exception)
    {
        _status = KernelTaskStatus.Failed;
        Exception = exception;
    }

    public KernelTask()
    {
        _status = KernelTaskStatus.Pending;
    }

    public Exception Exception { get; private set; }

    public bool IsCompleted => _status != KernelTaskStatus.Pending;

    public KernelTaskAwaiter GetAwaiter()
    {
        return new KernelTaskAwaiter(this);
    }

    internal bool TrySetResult()
    {
        if (_status != KernelTaskStatus.Pending)
        {
            return false;
        }
        else
        {
            _status = KernelTaskStatus.Success;
            _continuation?.Invoke();
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
            _continuation?.Invoke();
            return true;
        }
    }

    internal void RegisterContinuation(Action cont)
    {
        if (_status == KernelTaskStatus.Pending)
        {
            if (_continuation is null)
            {
                _continuation = cont;
            }
            else
            {
                var prev = _continuation;
                _continuation = () =>
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


public readonly struct KernelTaskAwaiter : INotifyCompletion
{
    private readonly KernelTask task;

    public KernelTaskAwaiter(KernelTask task)
    {
        this.task = task;
    }

    public bool IsCompleted => task.IsCompleted;

    public void GetResult()
    {

    }

    public void OnCompleted(Action continuation)
    {
        task.RegisterContinuation(continuation);
    }
}
