namespace Liquip.WASM.Instruction;

internal class I64store8 : Instruction
{
    public uint align;

    public I64store8(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return base.ToString() + "(offset = " + offset + ")";
    }
}
