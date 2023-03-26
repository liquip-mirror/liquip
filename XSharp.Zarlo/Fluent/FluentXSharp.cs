using XSharp.Assembler;


namespace XSharp.Zarlo.Fluent;

public class FluentXSharp
{
    public static FluentXSharp New()
    {
        return new FluentXSharp();
    }

    public FluentXSharp Comment(string comment)
    {
        XS.Comment(comment);
        return this;
    }

    public FluentXSharp Group(Action<FluentXSharp> content)
    {
        content(this);
        return this;
    }

    public FluentXSharp IfDef(string label, Action<FluentXSharp> content)
    {
        new LiteralAssemblerCode($"%ifdef {label}");
        content(this);
        new LiteralAssemblerCode($"%endif");
        return this;
    }
    

}