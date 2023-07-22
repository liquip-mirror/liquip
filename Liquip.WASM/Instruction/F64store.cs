namespace Liquip.WASM.Instruction;

internal class F64store : Instruction
{
    public uint align;

    public F64store(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return base.ToString() + "(offset = " + offset + ")";
    }
}
