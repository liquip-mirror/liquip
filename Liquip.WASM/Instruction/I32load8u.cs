namespace Liquip.WASM.Instruction;

internal class I32load8u : Instruction
{
    public uint align;

    public I32load8u(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return "i32.load8_u " + offset;
    }
}
