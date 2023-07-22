namespace Liquip.WASM.Instruction;

internal class I64load16s : Instruction
{
    public uint align;

    public I64load16s(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return base.ToString() + "(offset = " + offset + ")";
    }
}
