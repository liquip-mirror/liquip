using XSharp.Assembler;

namespace Liquip.XSharp.Fluent;

public static class LiteralAssemblerCodeEx
{
    public static FluentXSharpX86 LiteralCode(this FluentXSharpX86 me, string line, params object?[] args)
    {
        _ = new LiteralAssemblerCode(string.Format(line, args.ToArray()));
        return me;
    }

    public static FluentXSharpX86 LiteralCode(this FluentXSharpX86 me, string line)
    {
        _ = new LiteralAssemblerCode(line);
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
