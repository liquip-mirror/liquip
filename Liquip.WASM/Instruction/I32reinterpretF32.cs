﻿namespace Liquip.WASM.Instruction;

internal class I32reinterpretF32 : Instruction
{
    public I32reinterpretF32(Parser parser) : base(parser, true)
    {
    }
}