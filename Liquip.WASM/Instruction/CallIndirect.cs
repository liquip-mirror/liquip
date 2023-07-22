using System;

namespace Liquip.WASM.Instruction;

internal class CallIndirect : Instruction
{
    public int tableidx;
    private int typeidx;

    public CallIndirect(Parser parser) : base(parser, true)
    {
        typeidx = (int)parser.GetIndex();

        tableidx = (int)parser.GetUInt32();

        if (tableidx != 0x00)
        {
            Console.WriteLine("WARNING: call_indirect called with non-zero: 0x" + tableidx.ToString("X"));
        }
    }

    public override string ToString()
    {
        return "call_indirect";
    }
}
