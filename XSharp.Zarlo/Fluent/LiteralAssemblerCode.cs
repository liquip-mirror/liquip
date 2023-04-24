using XSharp.Assembler;

namespace XSharp.Zarlo.Fluent;

public static class LiteralAssemblerCodeEx
{
    public static FluentXSharpX86 LiteralCode(this FluentXSharpX86 me, string line)
    {
        _ = new LiteralAssemblerCode(line);
        return me;
    }

    public static FluentXSharpX86 LiteralCode(this FluentXSharpX86 me, params string[] lines)
    {
        foreach (var line in lines)
        {
            _ = new LiteralAssemblerCode(line);
        }

        return me;
    }

    public static FluentXSharpX86 LiteralCode(this FluentXSharpX86 me, IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            _ = new LiteralAssemblerCode(line);
        }

        return me;
    }
}