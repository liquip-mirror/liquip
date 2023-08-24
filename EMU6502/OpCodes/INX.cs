using EMU6502.Interface;

namespace EMU6502.OpCodes;

public class INX: IOpCode
{
    public OpCode OpCode { get; } = OpCode.INX;
    public void Execute(CPU cpu, AddressSpace addressSpace)
    {
        cpu.X++;
    }
}
