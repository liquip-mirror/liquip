using System.Runtime.CompilerServices;

namespace Cosmos.Zarlo.Threading.Tasks;

public abstract class Awaiter: INotifyCompletion
{
    public abstract void OnCompleted(Action continuation);

    public abstract void UnsafeOnCompleted(Action continuation);

    public bool IsCompleted { get; internal set; }
    public abstract void GetResult();
}

public abstract class Awaiter<T>: INotifyCompletion
{
    public abstract void OnCompleted(Action continuation);

    public abstract void UnsafeOnCompleted(Action continuation);

    public bool IsCompleted { get; internal set; }
    public abstract T GetResult();
}