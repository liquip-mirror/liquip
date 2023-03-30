using IL2CPU.API.Attribs;

namespace Cosmos.Zarlo.Plugs;

[Plug(Target = typeof(GC))]
public class GCPlug
{
    public static void _Collect(int generation, int mode)
    {
        Cosmos.Core.Memory.Heap.Collect();
    }
}