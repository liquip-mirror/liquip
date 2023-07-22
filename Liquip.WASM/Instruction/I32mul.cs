namespace Liquip.WASM.Instruction;

internal class I32mul : Instruction
{
    public I32mul(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "i32.mul";
    }
}
