namespace Liquip.WASM.Instruction;

internal class I32leu : Instruction
{
    public I32leu(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "i32.le_u";
    }
}
