using EMU6502.Interface;

namespace EMU6502.OpCodes;

public class LDX_IM: IOpCode
{
    public OpCode OpCode { get; } = OpCode.LDX_IM;
    public void Execute(CPU cpu, AddressSpace addressSpace)
    {
        cpu.X = addressSpace.ReadByte(cpu, cpu.PC);
    }
}
