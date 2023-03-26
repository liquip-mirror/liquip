using System.Runtime.CompilerServices;

namespace Cosmos.Zarlo.Threading.Tasks;

public class TaskMethodBuilder
{
    public static TaskMethodBuilder Create()
    {
        throw new NotImplementedException();
    }

    public void Start<TStateMachine>(ref TStateMachine stateMachine)
        where TStateMachine : IAsyncStateMachine
    {
    }

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
        
    }

    public void SetException(Exception exception)
    {
    }

    public void SetResult()
    {
        this.Task.GetAwaiter().IsCompleted = true;
    }

    public void AwaitOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : INotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
    }

    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
    }

    public Task Task { get; }
}