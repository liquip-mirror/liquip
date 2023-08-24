using EMU6502.Interface;

namespace EMU6502.OpCodes;

public class DEY: IOpCode
{
    public OpCode OpCode { get; } = OpCode.DEY;
    public void Execute(CPU cpu, AddressSpace addressSpace)
    {
        cpu.Y--;
    }
}
