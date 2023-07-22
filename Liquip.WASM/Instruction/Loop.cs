namespace Liquip.WASM.Instruction;

internal class Loop : Instruction
{
    public Loop(Parser parser) : base(parser, true)
    {
        type = parser.GetBlockType();
    }

    public override string ToString()
    {
        return "loop";
    }
}
