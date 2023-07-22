namespace Liquip.WASM.Instruction;

internal class I64store : Instruction
{
    public uint align;

    public I64store(Parser parser) : base(parser, true)
    {
        align = parser.GetUInt32();
        offset = parser.GetUInt32();
    }

    public override string ToString()
    {
        return "i64.store " + offset;
    }
}
