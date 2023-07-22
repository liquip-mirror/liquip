namespace Liquip.WASM.Instruction;

internal class I64const : Instruction
{
    public ulong value;

    public I64const(Parser parser) : base(parser, true)
    {
        value = (ulong)parser.GetInt64();
    }

    public override string ToString()
    {
        return "i64.const " + (long)value;
    }
}
