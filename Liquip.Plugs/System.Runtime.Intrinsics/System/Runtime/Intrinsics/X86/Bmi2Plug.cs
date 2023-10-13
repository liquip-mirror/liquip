using System.Runtime.Intrinsics.X86;
using IL2CPU.API;
using IL2CPU.API.Attribs;
using Liquip.XSharp;
using Liquip.XSharp.Fluent;
using XSharp;
using XSharp.Assembler;
using Registers = XSharp.Assembler.x86.Registers;

namespace Liquip.Plugs.System.Runtime.Intrinsics.System.Runtime.Intrinsics.X86;

/// <summary>
/// Bmi1Plug
/// </summary>
[Plug(target: typeof(Bmi2))]
public class Bmi2Plug
{
    /// <summary>
    ///
    /// </summary>
    public static bool get_IsSupported => CPUID.FeatureFlags.BMI2;

    /// <summary>
    ///   <para>unsigned int _andn_u32 (unsigned int a, unsigned int b)</para>
    ///   <para>ANDN r32a, r32b, reg/m32</para>
    /// </summary>
    /// <param name="left" />
    /// <param name="right" />
    [PlugMethod(Assembler = typeof(AndNotAsm))]
    public static uint AndNot(uint left, uint right) => throw new ImplementedInPlugException();

    /// <summary>
    /// asm plug
    /// </summary>
    public class AndNotAsm : AssemblerMethod
    {
        public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
        {
            var args = ArgumentBuilder.New();
            args.Add<uint>("left");
            args.Add<uint>("right");
            FluentXSharp.NewX86()
                .Set(XSRegisters.EBX, args.GetArg("left"))
                .Set(XSRegisters.ECX, args.GetArg("right"))
                .LiteralCode("ANDN {0}, {1}, {2}", nameof(XSRegisters.EAX), nameof(XSRegisters.EBX), nameof(XSRegisters.ECX))
                .Push(XSRegisters.EAX);
        }
    }



    /// <summary>
    ///   <para>unsigned int _bextr_u32 (unsigned int a, unsigned int start, unsigned int len)</para>
    ///   <para>BEXTR r32a, reg/m32, r32b</para>
    /// </summary>
    /// <param name="value" />
    /// <param name="start" />
    /// <param name="length" />
    public static uint BitFieldExtract(uint value, byte start, byte length)
        => BitFieldExtract(value, (ushort)((start << 8) + length));



    /// <summary>
    ///   <para>unsigned int _bextr2_u32 (unsigned int a, unsigned int control)</para>
    ///   <para>BEXTR r32a, reg/m32, r32b</para>
    /// </summary>
    /// <param name="value" />
    /// <param name="control" />
    [PlugMethod(Assembler = typeof(BitFieldExtractAsm))]
    public static uint BitFieldExtract(uint value, ushort control) => throw new ImplementedInPlugException();

    /// <summary>
    /// asm plug
    /// </summary>
    public class BitFieldExtractAsm : AssemblerMethod
    {
        public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
        {
            var args = ArgumentBuilder.New();
            args.Add<uint>("value");
            args.Add<ushort>("control");
            FluentXSharp.NewX86()
                .Set(XSRegisters.EBX, args.GetArg("value"))
                .Set(XSRegisters.ECX, args.GetArg("control"))
                .LiteralCode("BEXTR {0}, {1}, {2}", nameof(XSRegisters.EAX), nameof(XSRegisters.EBX), nameof(XSRegisters.ECX))
                .Push(XSRegisters.EAX);
        }
    }


    /// <summary>
    ///   <para>unsigned int _blsi_u32 (unsigned int a)</para>
    ///   <para>BLSI reg, reg/m32</para>
    /// </summary>
    /// <param name="value" />
    [PlugMethod(Assembler = typeof(ExtractLowestSetBitAsm))]
    public static uint ExtractLowestSetBit(uint value) => throw new ImplementedInPlugException();

    /// <summary>
    /// asm plug
    /// </summary>
    public class ExtractLowestSetBitAsm : AssemblerMethod
    {
        public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
        {
            var args = ArgumentBuilder.New();
            args.Add<uint>("value");
            FluentXSharp.NewX86()
                .Set(XSRegisters.EBX, args.GetArg("value"))
                .LiteralCode("BLSI {0}, {1}", nameof(XSRegisters.EAX), nameof(XSRegisters.EBX))
                .Push(XSRegisters.EAX);
        }
    }


    /// <summary>
    ///   <para>unsigned int _blsmsk_u32 (unsigned int a)</para>
    ///   <para>BLSMSK reg, reg/m32</para>
    /// </summary>
    /// <param name="value" />
    [PlugMethod(Assembler = typeof(GetMaskUpToLowestSetBitAsm))]
    public static uint GetMaskUpToLowestSetBit(uint value) => throw new ImplementedInPlugException();

    /// <summary>
    /// asm plug
    /// </summary>
    public class GetMaskUpToLowestSetBitAsm : AssemblerMethod
    {
        public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
        {
            var args = ArgumentBuilder.New();
            args.Add<uint>("value");
            FluentXSharp.NewX86()
                .Set(XSRegisters.EBX, args.GetArg("value"))
                .LiteralCode("BLSMSK {0}, {1}", nameof(XSRegisters.EAX), nameof(XSRegisters.EBX))
                .Push(XSRegisters.EAX);
        }
    }


    /// <summary>
    ///   <para>unsigned int _blsr_u32 (unsigned int a)</para>
    ///   <para>BLSR reg, reg/m32</para>
    /// </summary>
    /// <param name="value" />
    [PlugMethod(Assembler = typeof(ResetLowestSetBitAsm))]
    public static uint ResetLowestSetBit(uint value) => throw new ImplementedInPlugException();

    /// <summary>
    /// asm plug
    /// </summary>
    public class ResetLowestSetBitAsm : AssemblerMethod
    {
        public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
        {
            var args = ArgumentBuilder.New();
            args.Add<int>("value");
            FluentXSharp.NewX86()
                .Set(XSRegisters.EBX, args.GetArg("value"))
                .LiteralCode("BLSR {0}, {1}", nameof(XSRegisters.EAX), nameof(XSRegisters.EBX))
                .Push(XSRegisters.EAX);
        }
    }


    /// <summary>
    ///   <para>int _mm_tzcnt_32 (unsigned int a)</para>
    ///   <para>TZCNT reg, reg/m32</para>
    /// </summary>
    /// <param name="value" />
    [PlugMethod(Assembler = typeof(TrailingZeroCountAsm))]
    public static uint TrailingZeroCount(uint value) => throw new ImplementedInPlugException();

    /// <summary>
    /// asm plug
    /// </summary>
    public class TrailingZeroCountAsm : AssemblerMethod
    {
        public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
        {
            var args = ArgumentBuilder.New();
            args.Add<uint>("value");
            FluentXSharp.NewX86()
                .Set(XSRegisters.EBX, args.GetArg("value"))
                .LiteralCode("TZCNT {0}, {1}", nameof(XSRegisters.EAX), nameof(XSRegisters.EBX))
                .Push(XSRegisters.EAX);
        }
    }
}
