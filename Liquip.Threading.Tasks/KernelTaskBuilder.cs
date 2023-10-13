using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Liquip.Threading.Tasks;

public class KernelTaskBuilder
{
    private KernelTask? task;

    public static KernelTaskBuilder Create() => default;

    public KernelTask Task => task ??= new KernelTask();

    public void SetException(Exception e) => Task.TrySetException(e);

    public void SetResult() =>  Task.TrySetResult();

    public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : INotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
        awaiter.OnCompleted(stateMachine.MoveNext);
    }

    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
        awaiter.UnsafeOnCompleted(stateMachine.MoveNext);
    }

    public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
    {
        var move = stateMachine.MoveNext;
        ThreadPool.QueueUserWorkItem(_ =>
        {
            move();
        });

    }

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
        // nothing to do
    }

}
