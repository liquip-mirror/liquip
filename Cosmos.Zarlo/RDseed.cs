using Cosmos.Core;
using IL2CPU.API;
using IL2CPU.API.Attribs;
using XSharp;
using XSharp.Assembler;
using XSharp.Assembler.x86;
using XSharp.x86;
using XSharp.Zarlo.Fluent;
using static XSharp.XSRegisters;
using Label = XSharp.Zarlo.Fluent.Label;

namespace Cosmos.Zarlo;

public static class RDseed
{
    public static bool IsSupported()
    {
        if (CPU.CanReadCPUID() > 0)
        {
            //mov eax, 7     ; set EAX to request function 7
            //mov ecx, 0     ; set ECX to request subfunction 0
            //cpuid
            int eax = 0;
            int ebx = 0;
            int ecx = 0;
            int edx = 0;
            CPU.ReadCPUID(7, ref eax, ref ebx, ref ecx, ref edx);

            //shr ebx, 18
            var flag = ebx >> 18;
            //and ebx, 1 
            return (flag & 1) == 1;
        }

        return false;
    }

    // ReSharper disable once InconsistentNaming
    public static long GetRDSeed64()
    {
        return GetRDSeed32() << 32 | GetRDSeed32();
    }

    // ReSharper disable once InconsistentNaming
    [PlugMethod(Assembler = typeof(GetRDSeed32Asm))]
    public static int GetRDSeed32() => throw new ImplementedInPlugException(typeof(GetRDSeed32Asm));
}

public class GetRDSeed32Asm : AssemblerMethod
{
    public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
    {
        FluentXSharp.New()
            .Comment("GetRDSeed32")
            .Set(ECX, 100)
            .Label(".retry", out var retry)
            .Group(i =>
            {
                i.LiteralCode("rdseed eax")
                    .Jump(Label.Get(".done"), ConditionalTestEnum.Carry)
                    .Decrement(ECX)
                    .Jump(retry, ConditionalTestEnum.NotZero)
                    .Jump(retry);
            })
            .Label(".done")
            .Push(EAX);
    }
}