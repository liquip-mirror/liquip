namespace Liquip.WASM.Instruction;

internal class I32divs : Instruction
{
    public I32divs(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "i32.div_s";
    }
}
