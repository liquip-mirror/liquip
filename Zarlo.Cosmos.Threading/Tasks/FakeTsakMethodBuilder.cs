using System;
using System.Runtime.CompilerServices;

namespace Zarlo.Cosmos.Threading.Tasks;

public struct FakeStateMachine : IAsyncStateMachine
{
    public AsyncTaskMethodBuilder _methodBuilder; 

    public void MoveNext()
    {
        // Implementation
    }

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
        _methodBuilder.SetStateMachine(stateMachine);
    }
}

public class FakeTsakMethodBuilder
{
    private IAsyncStateMachine _stateMachine;
    private Action _moveNextRunner;

    public FakeTsakMethodBuilder()
        => Console.WriteLine(".ctor");

    public static FakeTsakMethodBuilder Create()
        => new FakeTsakMethodBuilder();

    public void SetResult() => Console.WriteLine("SetResult");

    public void Start<TStateMachine>(ref TStateMachine stateMachine)
        where TStateMachine : IAsyncStateMachine
    {
        Console.WriteLine("Start");
        stateMachine.MoveNext();
    }

    public FakeTask Task => default(FakeTask);

    // AwaitOnCompleted, AwaitUnsafeOnCompleted, SetException 
    // and SetStateMachine are empty
    public void AwaitOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : INotifyCompletion
        where TStateMachine : IAsyncStateMachine
    { }

    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion
        where TStateMachine : IAsyncStateMachine
    { }

    public void SetException(Exception e) { }

    public void SetStateMachine(IAsyncStateMachine stateMachine) => _stateMachine = stateMachine;
}

public class FakeTsakMethodBuilder_T<T>
{
    private IAsyncStateMachine _stateMachine;
    private Action _moveNextRunner;
    public FakeTsakMethodBuilder_T()
        => Console.WriteLine(".ctor");

    public static FakeTsakMethodBuilder_T<T> Create()
        => new FakeTsakMethodBuilder_T<T>();

    public void SetResult() => Console.WriteLine("SetResult");

    public void Start<TStateMachine>(ref TStateMachine stateMachine)
        where TStateMachine : IAsyncStateMachine
    {
        Console.WriteLine("Start");
        stateMachine.MoveNext();
    }

    public FakeTask<T> Task => default(FakeTask<T>);

    // AwaitOnCompleted, AwaitUnsafeOnCompleted, SetException 
    // and SetStateMachine are empty
    public void AwaitOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : INotifyCompletion
        where TStateMachine : IAsyncStateMachine
    { 
        awaiter.OnCompleted(_moveNextRunner);
    }

    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
        ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion
        where TStateMachine : IAsyncStateMachine
    { }

    public void SetException(Exception e) { }

    public void SetStateMachine(IAsyncStateMachine stateMachine) { }
}


