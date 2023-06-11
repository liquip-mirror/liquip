using XSharp.Assembler;


namespace Zarlo.XSharp.Fluent;

public class FluentXSharp
{

    public List<Label> UsedLabels = new List<Label>();

    [Obsolete()]
    public static FluentXSharp New()
    {
        return new FluentXSharp();
    }

    public static FluentXSharpX86 NewX86()
    {
        return new FluentXSharpX86();
    }

}


public class FluentXSharpX86 : FluentXSharp
{

}