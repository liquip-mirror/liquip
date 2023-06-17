using Zarlo.Cosmos.ELF;
using IL2CPU.API;
using IL2CPU.API.Attribs;
using XSharp;
using Zarlo.XSharp;
using Zarlo.XSharp.Fluent;
using static XSharp.XSRegisters;

namespace Zarlo.Cosmos.Plugs;


[Plug(Target = typeof(Invoker))]
public class InvokerPlug
{

    [Inline]
    public static unsafe void _CallCode(
        ref uint offset,
        ref uint* stack,
        ref uint eax,
        ref uint ebx,
        ref uint ecx,
        ref uint edx,
        ref uint esi,
        ref uint edi,
        ref uint esp,
        ref uint ebp
    )
    {
        var args = ArgumentBuilder.Inline();

        FluentXSharp.NewX86()

            .Comment("Load State")
            .SetPointer(EAX, args.GetArg(nameof(eax)))
            .SetPointer(EBX, args.GetArg(nameof(ebx)))
            .SetPointer(ECX, args.GetArg(nameof(ecx)))
            .SetPointer(EDX, args.GetArg(nameof(edx)))
            .SetPointer(EDI, args.GetArg(nameof(edi)))
            
            .SetPointer(args.GetArg(nameof(esp)), ESP)
            .SetPointer(args.GetArg(nameof(ebp)), EBP)
            .SetPointer(EAX, args.GetArg(nameof(stack)))
            .Add(EAX, 50)
            .Set(ESP, EAX)
            .Set(EBP, EAX)
            .SetPointer(EAX, args.GetArg(nameof(offset)))
            .Call(EAX)
            .SetPointer(ECX, args.GetArg(nameof(stack)))
            .SetPointer(args.GetArg(nameof(ecx)), EAX, XSRegisters.RegisterSize.Int32)
            .SetPointer(ESP, args.GetArg(nameof(esp)))
            .SetPointer(EBP, args.GetArg(nameof(ebp)))
            .Comment("DONE Load State")
            ;

    }
    
}