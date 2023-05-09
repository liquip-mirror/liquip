using Cosmos.Core;
using Cosmos.Core.Memory;

namespace Cosmos.Zarlo;


public class ZString : IDisposable
{

    public string Value;

    public ZString(string data)
    {
        Value = data;
    }

    public void Dispose()
    {
        unsafe
        {
            Heap.Free(GCImplementation.GetPointer(Value));
        }
    }
}