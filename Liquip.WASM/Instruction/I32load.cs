namespace Liquip.WASM.Instruction;

internal class I32load : Instruction
{
    public uint align;

    public I32load(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return "i32.load " + offset;
    }
}
