namespace Liquip.WASM.Instruction;

internal class F32load : Instruction
{
    public uint align;

    public F32load(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return base.ToString() + "(offset = " + offset + ")";
    }
}
