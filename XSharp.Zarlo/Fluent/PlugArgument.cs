namespace XSharp.Zarlo.Fluent;

public class PlugArgument
{
    public Type? Type { get; init; }
    public int Offset { get; init; }
    public string Name { get; init; }

    public PlugArgument(string name, int offset, Type? type)
    {
        Type = type;
        Offset = offset;
        Name = name;
    }
}