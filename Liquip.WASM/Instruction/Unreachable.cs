namespace Liquip.WASM.Instruction;

internal class Unreachable : Instruction
{
    public Unreachable(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "unreachable";
    }
}
