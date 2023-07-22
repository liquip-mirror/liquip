namespace Liquip.WASM.Instruction;

internal class Return : Instruction
{
    public Return(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "return";
    }
}
