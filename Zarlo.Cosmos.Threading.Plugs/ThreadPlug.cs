using IL2CPU.API.Attribs;
using XSharp.Assembler;

namespace Zarlo.Cosmos.Threading.Plugs;

[Plug(Target = typeof(Thread), IsOptional = false)]
public class ThreadPlug
{
    [PlugMethod(Assembler = typeof(DoYieldAsm), IsOptional = false)]
    public static void DoYield()
    {
    }
}

public class DoYieldAsm : AssemblerMethod
{
    public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
    {
        SwitchTaskAsm.SwitchTask();
    }
}
