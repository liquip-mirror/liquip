namespace Liquip.WASM.Instruction;

internal class I32gtu : Instruction
{
    public I32gtu(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "i32.gt_u";
    }
}
