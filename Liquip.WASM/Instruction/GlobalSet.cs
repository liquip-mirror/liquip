using System;
using System.Linq;

namespace Liquip.WASM.Instruction;

internal class GlobalSet : Instruction
{
    private readonly Global global;

    public GlobalSet(Parser parser) : base(parser, true)
    {
        index = (int)parser.GetUInt32();
        if (index >= parser.BaseModule.Globals.Count())
        {
            throw new Exception("Invalid global variable");
        }

        global = parser.BaseModule.Globals[index];
    }

    public override string ToString()
    {
        return "global.set " + global.Name + " (" + Type.Pretify(global.GetValue()) + ")";
    }
}
