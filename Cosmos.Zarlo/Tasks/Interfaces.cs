using System.Runtime.CompilerServices;

namespace Cosmos.Zarlo.Tasks;


public interface IAwaitable<out TResult>: IDisposable
{
    IAwaiter<TResult> GetAwaiter();
}

public interface IAwaitable: IDisposable
{
    IAwaiter GetAwaiter();
}

public interface IAwaiter<out TResult> : ICriticalNotifyCompletion
{
    bool IsCompleted { get; }
    TResult GetResult();
}

public interface IAwaiter : ICriticalNotifyCompletion
{
    bool IsCompleted { get; }
    void GetResult();
}