using XSharp.Assembler;
using XSharp.Zarlo.Fluent.FPU;
using XSharp.Zarlo.Fluent.SSE;

namespace XSharp.Zarlo.Fluent;

public static class MiscEx
{
    public static FluentXSharp Comment(this FluentXSharp me, string comment)
    {
        XS.Comment(comment);
        return me;
    }

    public static FluentXSharp Group(this FluentXSharp me, Action<FluentXSharp> content)
    {
        content(me);
        return me;
    }

    public static FluentXSharp IfDef(this FluentXSharp me, string label, Action<FluentXSharp> content)
    {
        _ = new LiteralAssemblerCode($"%ifdef {label}");
        content(me);
        _ = new LiteralAssemblerCode($"%endif");
        return me;
    }

    public static FluentXSharp Cpuid(this FluentXSharp me)
    {
        XS.Cpuid();
        return me;
    }
    
    public static FluentXSharp SSE(this FluentXSharp me, Action<FluentXSharpSSE> content)
    {
        content(new FluentXSharpSSE());
        return me;
    }

    public static FluentXSharp FPU(this FluentXSharp me, Action<FluentXSharpFPU> content)
    {
        content(new FluentXSharpFPU());
        return me;
    }

}