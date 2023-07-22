using System;

namespace Liquip.WASM.Instruction;

internal class MemoryGrow : Instruction
{
    public MemoryGrow(Parser parser) : base(parser, true)
    {
        var zero = parser.GetUInt32(); // May be used in future version of WebAssembly to address additional memories

        if (zero != 0x00)
        {
            Console.WriteLine("WARNING: memory.grow called with non-zero: 0x" + zero.ToString("X"));
        }
    }

    public override string ToString()
    {
        return "memory.grow";
    }
}
