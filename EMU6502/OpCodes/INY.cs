using EMU6502.Interface;

namespace EMU6502.OpCodes;

public class INY: IOpCode
{
    public OpCode OpCode { get; } = OpCode.INY;
    public void Execute(CPU cpu, AddressSpace addressSpace)
    {
        cpu.Y++;
    }
}
