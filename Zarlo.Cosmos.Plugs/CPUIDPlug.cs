using IL2CPU.API;
using IL2CPU.API.Attribs;
using Zarlo.XSharp;
using Zarlo.XSharp.Fluent;

using static XSharp.XSRegisters;

namespace Zarlo.Cosmos.Plugs;


[Plug(Target = typeof(CPUID))]
public class CPUIDPlug
{

    [Inline]
    public static void Raw(uint type, uint subType, ref int eax, ref int ebx, ref int ecx, ref int edx)
    {
        var args = ArgumentBuilder.Inline();
        FluentXSharp.NewX86()
            .SetPointer(EAX, args.GetArg(nameof(type)))
            .SetPointer(ECX, args.GetArg(nameof(subType)))
            .Cpuid()
            .SetPointer(EDI, args.GetArg(nameof(eax)))
            .Set(EDI, EAX, destinationIsIndirect: true)
            .SetPointer(EDI, args.GetArg(nameof(ebx)))
            .Set(EDI, EBX, destinationIsIndirect: true)
            .SetPointer(EDI, args.GetArg(nameof(ecx)))
            .Set(EDI, ECX, destinationIsIndirect: true)
            .SetPointer(EDI, args.GetArg(nameof(edx)))
            .Set(EDI, EDX, destinationIsIndirect: true);
    }

}
