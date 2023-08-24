using EMU6502.Interface;

namespace EMU6502.OpCodes;

public class LDA_IM: IOpCode
{
    public OpCode OpCode { get; } = OpCode.LDA_IM;
    public void Execute(CPU cpu, AddressSpace addressSpace)
    {
        cpu.A = addressSpace.ReadByte(cpu, cpu.PC);
    }
}
