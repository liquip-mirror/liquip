namespace Liquip.WASM.Instruction;

internal class I32ltu : Instruction
{
    public I32ltu(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "i32.lt_u";
    }
}
