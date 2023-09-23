using Cosmos.Core;
using IL2CPU.API.Attribs;
using XSharp.Assembler;
using XSharp.Assembler.x86;
using Liquip.XSharp;
using Liquip.XSharp.Fluent;
using static XSharp.XSRegisters;
using Label = Liquip.XSharp.Fluent.Label;

namespace Liquip;

public static class RDseed
{
    /// <summary>
    /// detect if RdSeed is supported
    /// </summary>
    /// <returns></returns>
    public static bool IsSupported() => CPUID.FeatureFlags.RDSEED;

    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// calls rdseed
    /// </summary>
    /// <returns></returns>
    public static long GetRDSeed64()
    {
        return (GetRDSeed32() << 32) | GetRDSeed32();
    }

    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// calls rdseed
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ImplementedInPlugException"></exception>
    [PlugMethod(Assembler = typeof(GetRDSeed32Asm))]
    public static int GetRDSeed32()
    {
        throw new ImplementedInPlugException(typeof(GetRDSeed32Asm));
    }
}

/// <summary>
///
/// </summary>
public class GetRDSeed32Asm : AssemblerMethod
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="aAssembler"></param>
    /// <param name="aMethodInfo"></param>
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
