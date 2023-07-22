﻿namespace Liquip.WASM.Instruction;

internal class I32ges : Instruction
{
    public I32ges(Parser parser) : base(parser, true)
    {
    }

    public override string ToString()
    {
        return "i32.ge_s";
    }
}
