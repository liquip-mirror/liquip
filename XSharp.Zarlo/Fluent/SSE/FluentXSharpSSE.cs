using XSharp.Assembler.x86;
using XSharp.Assembler.x86.SSE;

namespace XSharp.Zarlo.Fluent.SSE;

public class FluentXSharpSSE : FluentXSharp
{
    public FluentXSharpSSE AddSS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.RegisterXMM source)
    {
        XS.SSE.AddSS(destination, source);
        return this;
    }

    public FluentXSharpSSE MulSS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.RegisterXMM source)
    {
        XS.SSE.MulSS(destination, source);
        return this;
    }


    public FluentXSharpSSE SubSS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.RegisterXMM source)
    {
        XS.SSE.SubSS(destination, source);
        return this;
    }

    public FluentXSharpSSE XorPS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.RegisterXMM source)
    {
        XS.SSE.XorPS(destination, source);
        return this;
    }

    public FluentXSharpSSE CompareSS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.RegisterXMM source,
        ComparePseudoOpcodes comparision)
      {
        XS.SSE.CompareSS(destination, source, comparision);
        return this;
      }

      public FluentXSharpSSE ConvertSI2SS(
        XSRegisters.RegisterXMM destination,
        XSRegisters.Register32 source,
        bool sourceIsIndirect = false)
      {
          XS.SSE.ConvertSI2SS(destination, source, sourceIsIndirect);
          return this;
      }


    
}