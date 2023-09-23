using IL2CPU.API;
using IL2CPU.API.Attribs;
using Liquip.ELF;
using Liquip.XSharp;
using Liquip.XSharp.Fluent;
using static XSharp.XSRegisters;

namespace Liquip.Plugs;

/// <summary>
///
/// </summary>
[Plug(Target = typeof(Invoker))]
public class InvokerPlug
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="stack"></param>
    /// <param name="eax"></param>
    /// <param name="ebx"></param>
    /// <param name="ecx"></param>
    /// <param name="edx"></param>
    /// <param name="esi"></param>
    /// <param name="edi"></param>
    /// <param name="esp"></param>
    /// <param name="ebp"></param>
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
            .SetPointer(args.GetArg(nameof(ecx)), EAX, RegisterSize.Int32)
            .SetPointer(ESP, args.GetArg(nameof(esp)))
            .SetPointer(EBP, args.GetArg(nameof(ebp)))
            .Comment("DONE Load State")
            ;
    }
}
