using XSharp.Assembler.x86;

namespace XSharp.Zarlo.SSE4;


[XSharp.Assembler.OpCode("roundps")]
public class ROUNDPS: InstructionWithDestinationAndSourceAndSize {
    public ROUNDPS() : base("roundps")
    {
    }
}