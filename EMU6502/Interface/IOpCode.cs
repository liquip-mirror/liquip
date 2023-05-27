namespace EMU6502.Interface;

public interface IOpCode
{
    public void Execute(CPU cpu, AddressSpace addressSpace);
}
