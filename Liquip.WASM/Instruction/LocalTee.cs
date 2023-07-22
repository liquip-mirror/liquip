namespace Liquip.WASM.Instruction;

internal class LocalTee : Instruction
{
    public LocalTee(Parser parser) : base(parser, true)
    {
        index = (int)parser.GetUInt32();
    }

    public override string ToString()
    {
        return "local.tee $" + index;
    }
}
