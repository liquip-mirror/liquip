using System.Runtime.Intrinsics.X86;
using IL2CPU.API.Attribs;

namespace Liquip.Plugs.System.Runtime.Intrinsics.System.Runtime.Intrinsics.X86;

/// <summary>
/// X86BasePlug
/// </summary>
[Plug(target: typeof(X86Base))]
public class X86BasePlug
{

    /// <summary>
    /// raw cpuid call
    /// </summary>
    /// <param name="cpuInfo"></param>
    /// <param name="functionId"></param>
    /// <param name="subFunctionId"></param>
    private static unsafe void __cpuidex(int* cpuInfo, int functionId, int subFunctionId)
    {
        int Eax = 0;
        int Ebx = 0;
        int Ecx = 0;
        int Edx = 0;

        CPUID.Raw((uint)functionId, (uint)subFunctionId, ref Eax, ref Ebx, ref Ecx, ref Edx);

        cpuInfo[0] = Eax;
        cpuInfo[1] = Ebx;
        cpuInfo[2] = Ecx;
        cpuInfo[3] = Edx;
    }

}
