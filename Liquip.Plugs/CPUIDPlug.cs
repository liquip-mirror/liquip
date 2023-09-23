using IL2CPU.API;
using IL2CPU.API.Attribs;
using Liquip.XSharp;
using Liquip.XSharp.Fluent;
using static XSharp.XSRegisters;

namespace Liquip.Plugs;

/// <summary>
///
/// </summary>
[Plug(Target = typeof(CPUID))]
public class CPUIDPlug
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="type"></param>
    /// <param name="subType"></param>
    /// <param name="eax"></param>
    /// <param name="ebx"></param>
    /// <param name="ecx"></param>
    /// <param name="edx"></param>
    [Inline]
    public static void Raw(uint type, uint subType, ref int eax, ref int ebx, ref int ecx, ref int edx)
    {
        var args = ArgumentBuilder.Inline();
        FluentXSharp.NewX86()
            .SetPointer(EAX, args.GetArg(nameof(type)))
            .SetPointer(ECX, args.GetArg(nameof(subType)))
            .Cpuid()
            .SetPointer(EDI, args.GetArg(nameof(eax)))
            .Set(EDI, EAX, true)
            .SetPointer(EDI, args.GetArg(nameof(ebx)))
            .Set(EDI, EBX, true)
            .SetPointer(EDI, args.GetArg(nameof(ecx)))
            .Set(EDI, ECX, true)
            .SetPointer(EDI, args.GetArg(nameof(edx)))
            .Set(EDI, EDX, true);
    }
}
