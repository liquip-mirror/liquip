using IL2CPU.API;
using IL2CPU.API.Attribs;
using XSharp.Assembler;

namespace Liquip.Threading.Plugs;

[Plug(Target = typeof(Liquip.Threading.Thread), IsOptional = false)]
public class LiquipThreadPlug
{

    [Inline]
    public static void Yield()
    {
        SwitchTaskAsm.SwitchTask();
    }

}


