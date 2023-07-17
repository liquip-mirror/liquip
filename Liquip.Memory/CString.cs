using Cosmos.Core;

namespace Liquip.Memory;

public class CString: IDisposable
{

    public Pointer Pointer;

    public unsafe CString(char* ptr, uint length)
    {
        Pointer = Pointer.MakeFrom((uint*)ptr, length);
    }

    public void Dispose()
    {
    }
}
