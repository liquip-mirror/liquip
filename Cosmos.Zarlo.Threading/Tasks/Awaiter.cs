namespace Cosmos.Zarlo.Threading.Tasks;

public class Awaiter
{
    public void OnCompleted(Action continuation)
    {
        throw new NotImplementedException();
    }

    public void UnsafeOnCompleted(Action continuation)
    {
        throw new NotImplementedException();
    }

    public bool IsCompleted { get; internal set; }
    public void GetResult()
    {
        throw new NotImplementedException();
    }
}