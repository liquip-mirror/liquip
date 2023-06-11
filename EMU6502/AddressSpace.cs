using Zarlo.Cosmos.Memory;
using EMU6502.Interface;

namespace EMU6502;

public class AddressSpace: IDisposable
{

    private Pointer pointer;

    Dictionary<ushort, IBusItem> Bus = new Dictionary<ushort, IBusItem>();

    public AddressSpace()
    {
        pointer = Pointer.New(1024 * 64);
    }


    public byte ReadByte(CPU cpu, ushort address, bool step = true)
    { 
        unsafe
        {
            byte data;
            if (Bus.ContainsKey(address))
            {
                data = Bus[address].ReadByte(cpu, this, address);
            }
            else
            { 
                data = ((byte*)pointer)[cpu.PC];
            }
            if(step) cpu.PC++;
            cpu.Cycle++;
            return data;
        }
    }

    public ushort ReadWord(CPU cpu, ushort address, bool step = true)
    { 
        unsafe
        {
            ushort data;
            if (Bus.ContainsKey(address))
            {
                data = Bus[address].ReadWord(cpu, this, address);
            }
            else
            { 
                data = ((ushort*)pointer)[cpu.PC];
            }
            if(step) cpu.PC+=2;
            cpu.Cycle+=2;
            return data;
        }
    }


    public void WriteByte(CPU cpu, ushort address, byte value, bool step = true)
    { 
        unsafe
        {
            if (Bus.ContainsKey(address))
            {
                Bus[address].WriteByte(cpu, this, address, value);
            }
            else
            { 
                ((byte*)pointer)[cpu.PC] = value;
            }
            if(step) cpu.PC++;
            cpu.Cycle++;
            
        }
    }

    public void WriteWord(CPU cpu, ushort address, ushort value, bool step = true)
    { 
        unsafe
        {
            if (Bus.ContainsKey(address))
            {
                Bus[address].WriteWord(cpu, this, address, value);
            }
            else
            { 
                ((ushort*)pointer)[cpu.PC] = value;
            }
            if(step) cpu.PC+=2;
            cpu.Cycle+=2;
        }
    }

    public void Dispose()
    {
        pointer.Dispose();
    }

}
