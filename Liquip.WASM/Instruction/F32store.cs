namespace Liquip.WASM.Instruction;

internal class F32store : Instruction
{
    public uint align;

    public F32store(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return base.ToString() + "(offset = " + offset + ")";
    }
}
