using XSharp;

namespace Zarlo.XSharp.Fluent.SSE;

public static class MoveEx
{
    public static FluentXSharpX86SSE MoveSS(
        this FluentXSharpX86SSE me,
        XSRegisters.RegisterXMM destination,
        XSRegisters.RegisterXMM source)
    {
        XS.SSE.MoveSS(destination, source);
        return me;
    }

    public static FluentXSharpX86SSE MoveSS(
        this FluentXSharpX86SSE me, 
        XSRegisters.RegisterXMM destination,
        XSRegisters.Register32 source,
        bool sourceIsIndirect = false)
    {
        XS.SSE.MoveSS(destination, source, sourceIsIndirect);
        return me;
    }

    public static FluentXSharpX86SSE MoveSS(
        this FluentXSharpX86SSE me, 
        XSRegisters.Register32 destination,
        XSRegisters.RegisterXMM source,
        bool destinationIsIndirect = false)
    {
        XS.SSE.MoveSS(destination, source, destinationIsIndirect);
        return me;
    }

    public static FluentXSharpX86SSE MoveSS(
        this FluentXSharpX86SSE me, 
        XSRegisters.RegisterXMM destination,
        string sourceLabel,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null)
    {
        XS.SSE.MoveSS(destination, sourceLabel, destinationIsIndirect, destinationDisplacement, sourceIsIndirect, sourceDisplacement);
        return me;
    }

}