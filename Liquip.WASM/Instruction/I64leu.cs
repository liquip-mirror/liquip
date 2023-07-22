namespace Liquip.WASM.Instruction;

internal class I64leu : Instruction
{
    public I64leu(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "i64.le_u";
    }
}
