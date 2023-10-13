using System;
using Cosmos.Core;

namespace Liquip.Memory;

public class CString: IDisposable
{

    /// <summary>
    /// the raw pointer
    /// </summary>
    public Pointer Pointer;

    public bool AutoCleanUp { get; private set; }

    public unsafe CString(char* ptr, uint length)
    {
        Pointer = Pointer.MakeFrom((uint*)ptr, length);
        AutoCleanUp = false;
    }


    public void Dispose()
    {
        if (AutoCleanUp)
        {
            Pointer.Free();
        }
    }
}
