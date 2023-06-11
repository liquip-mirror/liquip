using Cosmos.Core.Memory;
using IL2CPU.API.Attribs;
namespace Zarlo.Cosmos.Plugs;

[Plug(Target = typeof(GC))]
public class GCPlug
{
    public static void _Collect(int generation, int mode)
    {
        Heap.Collect();
    }
}