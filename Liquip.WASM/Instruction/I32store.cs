namespace Liquip.WASM.Instruction;

internal class I32store : Instruction
{
    public uint align;

    public I32store(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return "i32.store " + offset;
    }
}
