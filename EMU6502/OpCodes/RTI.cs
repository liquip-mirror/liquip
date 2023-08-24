using EMU6502.Interface;

namespace EMU6502.OpCodes;

public class RTI: IOpCode
{
    public OpCode OpCode { get; } = OpCode.RTI;
    public void Execute(CPU cpu, AddressSpace addressSpace)
    {
        cpu.PC = cpu.PopWordToStack();
    }
}
