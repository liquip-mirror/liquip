namespace EMU6502.Interface;

public interface IBusItem
{

    public byte ReadByte(CPU cpu, AddressSpace addressSpace, ushort address);

    public ushort ReadWord(CPU cpu, AddressSpace addressSpace, ushort address);

    public void WriteByte(CPU cpu, AddressSpace addressSpace, ushort address, byte value);

    public void WriteWord(CPU cpu, AddressSpace addressSpace, ushort address, ushort value);

}
