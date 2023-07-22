namespace Liquip.WASM.Instruction;

internal class Nop : Instruction
{
    public Nop(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "nop";
    }
}
