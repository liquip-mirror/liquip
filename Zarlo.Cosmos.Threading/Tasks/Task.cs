namespace Zarlo.Cosmos.Threading.Tasks;

public abstract class Task
{
    public static FakeTask Completed {
        get
        {
            var t = new FakeTask();
            t.GetAwaiter().IsCompleted = true;
            return t;
        }
    }
    public abstract void Dispose();

    public abstract Awaiter GetAwaiter();
}

public abstract class Task<T>
{
    public static FakeTask<T> FromResult(T result)
    {
        return new FakeTask<T>(result);
    }

    public abstract void Dispose();

    public abstract Awaiter<T> GetAwaiter();
}