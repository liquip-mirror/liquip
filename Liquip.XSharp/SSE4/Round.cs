using XSharp.Assembler;
using XSharp.Assembler.x86;

namespace Liquip.XSharp.SSE4;

[OpCode("roundps")]
public class ROUNDPS : InstructionWithDestinationAndSourceAndSize
{
    public ROUNDPS() : base("roundps")
    {
    }
}
