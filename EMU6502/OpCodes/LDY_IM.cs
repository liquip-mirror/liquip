using EMU6502.Interface;

namespace EMU6502.OpCodes;

public class LDY_IM: IOpCode
{
    public OpCode OpCode { get; } = OpCode.LDY_IM;
    public void Execute(CPU cpu, AddressSpace addressSpace)
    {
        cpu.Y = addressSpace.ReadByte(cpu, cpu.PC);
    }
}
