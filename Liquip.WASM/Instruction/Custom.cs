using System;

namespace Liquip.WASM.Instruction;

internal class Custom : Instruction
{
    private Action a;

    public Custom(Action a) : base(null, true)
    {
        this.a = a;
    }
}
