namespace Liquip.WASM.Instruction;

internal class F64reinterpretI64 : Instruction
{
    public F64reinterpretI64(Parser parser) : base(parser, true)
    {
    }
}
