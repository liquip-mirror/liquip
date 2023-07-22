namespace Liquip.WASM.Instruction;

internal class I32ctz : Instruction
{
    public I32ctz(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "i32.ctz";
    }
}
