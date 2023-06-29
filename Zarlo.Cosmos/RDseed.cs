using Cosmos.Core;
using IL2CPU.API.Attribs;
using XSharp.Assembler;
using XSharp.Assembler.x86;
using Zarlo.XSharp;
using Zarlo.XSharp.Fluent;
using static XSharp.XSRegisters;
using Label = Zarlo.XSharp.Fluent.Label;

namespace Zarlo.Cosmos;

public static class RDseed
{
    public static bool IsSupported()
    {
        if (CPU.CanReadCPUID() > 0)
        {
            //mov eax, 7     ; set EAX to request function 7
            //mov ecx, 0     ; set ECX to request subfunction 0
            //cpuid
            var eax = 0;
            var ebx = 0;
            var ecx = 0;
            var edx = 0;
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
        return (GetRDSeed32() << 32) | GetRDSeed32();
    }

    // ReSharper disable once InconsistentNaming
    [PlugMethod(Assembler = typeof(GetRDSeed32Asm))]
    public static int GetRDSeed32()
    {
        throw new ImplementedInPlugException(typeof(GetRDSeed32Asm));
    }
}

public class GetRDSeed32Asm : AssemblerMethod
{
    public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
    {
        var done = Label.Get(".done");
        FluentXSharp.NewX86()
            .Comment("GetRDSeed32")
            .Set(ECX, 100)
            .Label(".retry", out var retry)
            .Group(i =>
            {
                i
                    .LiteralCode($@"rdseed {EAX.Name}")
                    .Jump(done, ConditionalTestEnum.Carry)
                    .Decrement(ECX)
                    .Jump(retry, ConditionalTestEnum.NotZero)

                    // return 0
                    .Set(EAX, 0)
                    .Push(EAX);
            })
            .Label(done)
            .Push(EAX);
    }
}
