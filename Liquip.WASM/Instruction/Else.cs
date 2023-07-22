namespace Liquip.WASM.Instruction;

internal class Else : Instruction
{
    public Instruction label;

    public Else(Parser parser) : base(parser, true)
    {
    }

    public override void End(Instruction end)
    {
        label = end;
    }

    public override string ToString()
    {
        return "else";
    }
}
