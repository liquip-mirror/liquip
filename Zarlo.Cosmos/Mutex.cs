using Cosmos.Core;
using Zarlo.Cosmos;
using Zarlo.Cosmos.Memory;

namespace Zarlo.Cosmos;

public class Mutex
{

    public static void Lock(object ptr)
    {
        unsafe
        {
            Lock(GCImplementation.GetPointer(ptr));
        }
    }

    public static void Lock(Pointer ptr)
    {
        unsafe
        {
            Lock((uint*)ptr);
        }
    }
    
    public static unsafe void Lock(uint* ptr)
    {

        while (ptr[0] == 1)
        { }

        ptr[0] = 1;

    }
    

    public static void Free(object ptr)
    {
        unsafe
        {
            Free(GCImplementation.GetPointer(ptr));
        }
    }

    public static void Free(Pointer ptr)
    {
        unsafe
        {
            Free((uint*)ptr);
        }
    }

    public static unsafe void Free(uint* ptr)
    {
        ptr[0] = 0;
    }
    

}