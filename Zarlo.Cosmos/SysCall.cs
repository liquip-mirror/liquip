using IL2CPU.API;
using static XSharp.XSRegisters;

using Zarlo.XSharp.Fluent;
using Zarlo.XSharp;
using IL2CPU.API.Attribs;

namespace Zarlo.Cosmos;

[Plug(Target = typeof(SysCall))]
public static class SysCall {

    [Inline]
    public static void Call(
        int call,
        ref uint _EDI,
        ref uint _ESI,
        ref uint _EBP,
        // ref uint _ESP,
        ref uint _EBX,
        ref uint _EDX,
        ref uint _ECX,
        ref uint _EAX
    ) {
        var args = ArgumentBuilder.Inline();
        FluentXSharp.NewX86()
        .SetPointer(EDI, args.GetArg(nameof(_EDI)))
        .SetPointer(ESI, args.GetArg(nameof(_ESI)))
        .SetPointer(EBP, args.GetArg(nameof(_EBP)))
        // .SetPointer(ESP, args.GetArg(nameof(_ESP)))
        .SetPointer(EBX, args.GetArg(nameof(_EBX)))
        .SetPointer(EDX, args.GetArg(nameof(_EDX)))
        .SetPointer(ECX, args.GetArg(nameof(_ECX)))
        .SetPointer(EAX, args.GetArg(nameof(_EAX)))
        .SetPointer(EDI, args.GetArg(nameof(_EDI)))

        .Interrupt(80)

        .SetPointer(args.GetArg(nameof(_ESI)), ESI)
        .SetPointer(args.GetArg(nameof(_EBP)), EBP)
        // .SetPointer(args.GetArg(nameof(_ESP)), ESP)
        .SetPointer(args.GetArg(nameof(_EBX)), EBX)
        .SetPointer(args.GetArg(nameof(_EDX)), EDX)
        .SetPointer(args.GetArg(nameof(_ECX)), ECX)
        .SetPointer(args.GetArg(nameof(_EAX)), EAX);

    }

}