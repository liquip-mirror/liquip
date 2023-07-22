namespace Liquip.WASM.Instruction;

internal class F64load : Instruction
{
    public uint align;

    public F64load(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return base.ToString() + "(offset = " + offset + ")";
    }
}
