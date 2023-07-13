using XSharp;

namespace Liquip.XSharp.Fluent;

public class PlugArgument
{
    public static readonly XSRegisters.Register32 Register = XSRegisters.EBP;

    public PlugArgument(string name, int offset, Type? type)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception();
        }

        Type = type;
        Offset = offset;
        Name = name;
    }

    public Type? Type { get; init; }
    public int Offset { get; init; }
    public string Name { get; init; }
}
