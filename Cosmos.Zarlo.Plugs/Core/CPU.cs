using Cosmos.Core;
using Cosmos.Zarlo.Core;
using IL2CPU.API.Attribs;

namespace Cosmos.Zarlo.Plugs.Core;

[Plug(Target = typeof(CPU), IsOptional = false)]
public class CPUImpl
{

    [PlugMethod(Assembler = typeof(CPUUpdateIDTAsm), IsOptional = false)]
    public static void UpdateIDT(bool aEnableInterruptsImmediately) => throw new ImplementedInPlugException();

}