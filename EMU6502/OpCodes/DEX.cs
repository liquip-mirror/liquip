using EMU6502.Interface;

namespace EMU6502.OpCodes;

public class DEX: IOpCode
{
    public OpCode OpCode { get; } = OpCode.DEX;
    public void Execute(CPU cpu, AddressSpace addressSpace)
    {
        cpu.X--;
    }
}
