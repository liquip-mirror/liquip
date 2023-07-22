namespace Liquip.WASM.Instruction;

internal class Br : Instruction
{
    public uint labelidx;

    public Br(Parser parser) : base(parser, true)
    {
        labelidx = parser.GetIndex();
    }

    public override string ToString()
    {
        return "br";
    }
}
