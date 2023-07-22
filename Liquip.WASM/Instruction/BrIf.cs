namespace Liquip.WASM.Instruction;

internal class BrIf : Instruction
{
    public uint labelidx;

    public BrIf(Parser parser) : base(parser, true)
    {
        labelidx = parser.GetIndex();
    }

    public override string ToString()
    {
        return "br_if";
    }
}
