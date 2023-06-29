using XSharp;
using XSharp.Assembler;
using Zarlo.XSharp.Fluent.FPU;
using Zarlo.XSharp.Fluent.SSE;

namespace Zarlo.XSharp.Fluent;

public static class MiscEx
{
    public static FluentXSharpX86 Comment(this FluentXSharpX86 me, string comment)
    {
        XS.Comment(comment);
        return me;
    }

    public static FluentXSharpX86 Group(this FluentXSharpX86 me, Action<FluentXSharpX86> content)
    {
        content(me);
        return me;
    }

    public static FluentXSharpX86 IfDef(this FluentXSharpX86 me, string label, Action<FluentXSharpX86> content)
    {
        _ = new LiteralAssemblerCode($"%ifdef {label}");
        content(me);
        _ = new LiteralAssemblerCode("%endif");
        return me;
    }

    public static FluentXSharpX86 Cpuid(this FluentXSharpX86 me)
    {
        XS.Cpuid();
        return me;
    }

    public static FluentXSharpX86 SSE(this FluentXSharpX86 me, Action<FluentXSharpX86SSE> content)
    {
        content(new FluentXSharpX86SSE());
        return me;
    }

    public static FluentXSharpX86 FPU(this FluentXSharpX86 me, Action<FluentXSharpFPU> content)
    {
        content(new FluentXSharpFPU());
        return me;
    }

    public static FluentXSharpX86 Interrupt(this FluentXSharpX86 me, byte interrupt)
    {
        XS.LiteralCode(string.Format("INT {0:X}", interrupt));
        return me;
    }
}
