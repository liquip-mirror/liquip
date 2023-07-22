namespace Liquip.WASM.Instruction;

internal class I32store16 : Instruction
{
    public uint align;

    public I32store16(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return "i32.store16 " + offset;
    }
}
