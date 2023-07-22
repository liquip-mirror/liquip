namespace Liquip.WASM.Instruction;

internal class I32const : Instruction
{
    public uint value;

    public I32const(Parser parser) : base(parser, true)
    {
        value = (uint)parser.GetInt32();
    }

    public override string ToString()
    {
        return "i32.const " + (int)value;
    }
}
