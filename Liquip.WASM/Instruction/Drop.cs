namespace Liquip.WASM.Instruction;

internal class Drop : Instruction
{
    public Drop(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "drop";
    }
}
