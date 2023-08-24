namespace EMU6502.Interface;

public interface IOpCode
{
    public OpCode OpCode { get; }
    public void Execute(CPU cpu, AddressSpace addressSpace);
}
