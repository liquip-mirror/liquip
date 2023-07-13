using XSharp;
using XSharp.Assembler.x86.SSE;

namespace Liquip.XSharp.Fluent.SSE;

public class FluentXSharpX86SSE
{
    public FluentXSharpX86SSE AddSS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.RegisterXMM source)
    {
        XS.SSE.AddSS(destination, source);
        return this;
    }

    public FluentXSharpX86SSE MulSS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.RegisterXMM source)
    {
        XS.SSE.MulSS(destination, source);
        return this;
    }


    public FluentXSharpX86SSE SubSS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.RegisterXMM source)
    {
        XS.SSE.SubSS(destination, source);

        return this;
    }

    public FluentXSharpX86SSE XorPS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.RegisterXMM source)
    {
        XS.SSE.XorPS(destination, source);
        return this;
    }

    public FluentXSharpX86SSE CompareSS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.RegisterXMM source,
        ComparePseudoOpcodes comparision)
    {
        XS.SSE.CompareSS(destination, source, comparision);
        return this;
    }

    public FluentXSharpX86SSE ConvertSI2SS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.Register32 source,
        bool sourceIsIndirect = false)
    {
        XS.SSE.ConvertSI2SS(destination, source, sourceIsIndirect);
        return this;
    }
}
