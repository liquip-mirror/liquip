namespace Liquip.WASM.Instruction;

internal class Block : Instruction
{
    public Instruction label;

    public Block(Parser parser) : base(parser, true)
    {
        type = parser.GetBlockType();
    }

    public override void End(Instruction end)
    {
        label = end;
    }

    public override string ToString()
    {
        return "block";
    }
}
