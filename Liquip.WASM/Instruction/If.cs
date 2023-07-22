namespace Liquip.WASM.Instruction;

internal class If : Instruction
{
    public Instruction label;

    public If(Parser parser) : base(parser, true)
    {
        type = parser.GetBlockType();
    }

    public override void End(Instruction end)
    {
        label = end;
    }

    public override string ToString()
    {
        return "if";
    }
}
