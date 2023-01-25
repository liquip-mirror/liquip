using Cosmos.Core;
using IL2CPU.API;
using IL2CPU.API.Attribs;
using XSharp;
using XSharp.Assembler;
using XSharp.Assembler.x86;
using XSharp.x86;
using static XSharp.XSRegisters;

namespace Cosmos.Zarlo;

public static class RDseed
{

    public static bool IsSuppoted() {
        if(CPU.CanReadCPUID() > 0)
        {
            //mov eax, 7     ; set EAX to request function 7
            //mov ecx, 0     ; set ECX to request subfunction 0
            //cpuid
            int eax = 7;
            int ebx = 0;
            int ecx = 0;
            int edx = 0; 
            CPU.ReadCPUID(0, ref eax, ref ebx, ref ecx, ref edx);

            //shr ebx, 18
            var flag = ebx >> 18;
            //and ebx, 1 
            return (flag & 1) == 1;
        }
        return false;
    }

    public static long GetRDSeed64()
    {
        return GetRDSeed32() << 32 | GetRDSeed32();
    }

    [PlugMethod(Assembler = typeof(GetRDSeed32Asm))]
    public static int GetRDSeed32()
    {        
        throw new NotImplementedException();
    }

    [PlugMethod(Assembler = typeof(GetRDSeed16Asm))]
    public static short GetRDSeed16()
    {
        throw new NotImplementedException();
    }

}


public class GetRDSeed16Asm: AssemblerMethod
{
    public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
    {
        XS.Comment("GetRDSeed16");
        XS.Set(ECX, 100);
        XS.Label(".retry");
        XS.LiteralCode("rdseed ax");
        XS.Jump(ConditionalTestEnum.Carry, ".done");
        XS.Decrement(ECX);
        XS.Jump(ConditionalTestEnum.NotZero, ".retry");
        XS.Jump(".retry");
        XS.Label(".done");
        XS.Push(AX);
    }
}

public class GetRDSeed32Asm: AssemblerMethod
{
    public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
    {
        XS.Comment("GetRDSeed32");
        XS.Set(ECX, 100);
        /* Do the 'loop' */
        XS.Label(".retry");
        XS.LiteralCode("rdseed eax");
        XS.Jump(ConditionalTestEnum.Carry, ".done");
        XS.Decrement(ECX);
        XS.Jump(ConditionalTestEnum.NotZero, ".retry");
        XS.Jump(".retry");
        XS.Label(".done");
        XS.Push(EAX);
    }
}