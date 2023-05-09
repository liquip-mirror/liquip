using IL2CPU.API;
using IL2CPU.API.Attribs;

namespace Cosmos.Zarlo.Core;


public static unsafe class ObjUtilities
{
    public static uint GetPointer(Delegate aVal)
    {
        return (uint)aVal.GetHashCode();
    }

    [PlugMethod(PlugRequired = true)]
    public static uint GetPointer(Object aVal) { return 0; }

    [PlugMethod(PlugRequired = true)]
    public static uint GetEntryPoint() { return 0; }
}