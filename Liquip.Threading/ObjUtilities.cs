using System;
using IL2CPU.API.Attribs;

namespace Liquip.Threading;

public static class ObjUtilities
{
    public static uint GetPointer(Delegate aVal)
    {
        return (uint)aVal.GetHashCode();
    }

    [PlugMethod(PlugRequired = true)]
    public static uint GetPointer(object aVal)
    {
        return 0;
    }

    [PlugMethod(PlugRequired = true)]
    public static uint GetEntryPoint()
    {
        return 0;
    }
}
