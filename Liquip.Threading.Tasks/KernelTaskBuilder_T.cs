using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Cosmos.System.Coroutines;

namespace Liquip.Threading.Tasks;

public class KernelTaskBuilder<T>
{
    private KernelTask<T>? task;

    public static KernelTaskBuilder<T> Create() => default;

    public KernelTask<T> Task => task ??= new KernelTask<T>();

    public void SetException(Exception e) => Task.TrySetException(e);

    public void SetResult(T result) =>  Task.TrySetResult(result);

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

        IEnumerator<CoroutineControlPoint?> run()
        {
            move();
            yield return new WaitUntil(() => true);
        }
        CoroutinePool.Main.AddCoroutine(new Coroutine(run()));

    }

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
        // nothing to do
    }

}
