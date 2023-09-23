using System;
using Cosmos.Core.Memory;
using IL2CPU.API.Attribs;

namespace Liquip.Plugs.System.Runtime.System;

/// <summary>
/// GCPlug
/// </summary>
[Plug(Target = typeof(GC))]
public class GCPlug
{
    /// <summary>
    /// Collect
    /// </summary>
    /// <param name="generation"></param>
    /// <param name="mode"></param>
    public static void _Collect(int generation, int mode)
    {
        Heap.Collect();
    }
}
