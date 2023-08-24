using EMU6502.Interface;

namespace EMU6502.OpCodes;

public class NOP : IOpCode
{
    public OpCode OpCode { get; } = OpCode.NOP;

    public void Execute(CPU cpu, AddressSpace addressSpace)
    {
        // NOP do nothing
    }
}
