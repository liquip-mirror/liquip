using XSharp.Assembler;

namespace XSharp.Zarlo.Fluent;

public static class LiteralAssemblerCodeEx
{
        
    public static FluentXSharp LiteralCode(this FluentXSharp me, string line)
    {
        _ = new LiteralAssemblerCode(line);
        return me;
    }

    public static FluentXSharp LiteralCode(this FluentXSharp me, params string[] lines)
    {
        foreach (var line in lines)
        {
            _ = new LiteralAssemblerCode(line);
        }

        return me;
    }
    
    public static FluentXSharp LiteralCode(this FluentXSharp me, IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            _ = new LiteralAssemblerCode(line);
        }
        return me;
    }
}