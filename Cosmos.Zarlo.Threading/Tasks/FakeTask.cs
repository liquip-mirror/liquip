using System.Runtime.CompilerServices;

namespace Cosmos.Zarlo.Threading.Tasks;

[AsyncMethodBuilder(typeof(FakeTsakMethodBuilder_T<>))]
public class FakeTask<T> : Task<T>
{
    private T _result;

    public FakeTask(T result)
    {
        _result = result;
    }

    public override void Dispose()
    {
    }

    public override Awaiter<T> GetAwaiter()
    {
        return new FakeAwaiter<T>(_result);
    }
}

public class FakeAwaiter<T> : Awaiter<T>, INotifyCompletion 
{
    private T _result;

    public FakeAwaiter(T result)
    {
        _result = result;
        this.IsCompleted = true;
    }

    public override void OnCompleted(Action continuation)
    {
        continuation();
    }

    public override void UnsafeOnCompleted(Action continuation)
    {
        continuation();
    }

    public override T GetResult() => _result;
}

[AsyncMethodBuilder(typeof(FakeTsakMethodBuilder))]
public class FakeTask : Task
{
    public FakeTask()
    {

    }

    public override void Dispose()
    {
    }

    public override Awaiter GetAwaiter()
    {
        return new FakeAwaiter();
    }
}

public class FakeAwaiter : Awaiter, INotifyCompletion 
{

    public FakeAwaiter()
    {
        this.IsCompleted = true;
    }

    public override void OnCompleted(Action continuation)
    {
        continuation();
    }

    public override void UnsafeOnCompleted(Action continuation)
    {
        continuation();
    }

    public override void GetResult()
    {
    }
}