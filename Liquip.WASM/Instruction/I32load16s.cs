namespace Liquip.WASM.Instruction;

internal class I32load16s : Instruction
{
    public uint align;

    public I32load16s(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return "i32.load16_s " + offset;
    }
}
