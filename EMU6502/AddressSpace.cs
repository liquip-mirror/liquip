using EMU6502.Interface;
using Liquip.Memory;

namespace EMU6502;

public class AddressSpace: IDisposable
{

    private Pointer pointer;

    Dictionary<ushort, IBusItem> Bus = new Dictionary<ushort, IBusItem>();

    public void RegisterBusItem(ushort address, IBusItem busItem)
    {
        Bus.Add(address, busItem);
    }

    public void RegisterBusItem(ushort startAddress, ushort endAddress, IBusItem busItem)
    {
        if (endAddress < startAddress) throw new ArgumentOutOfRangeException(nameof(endAddress));
        for (var i = startAddress; i < endAddress; i++)
        {
            RegisterBusItem(i, busItem);
        }
    }

    public AddressSpace()
    {
        pointer = Pointer.New(1024 * 64);
    }


    /// <summary>
    /// reads a byte from the given address
    /// </summary>
    /// <param name="cpu"></param>
    /// <param name="address"></param>
    /// <param name="step"></param>
    /// <returns></returns>
    public byte ReadByte(CPU cpu, ushort address, bool step = true)
    {
        unsafe
        {
            byte data;
            if (Bus.TryGetValue(address, out var value))
            {
                data = value.ReadByte(cpu, this, address);
            }
            else
            {
                data = ((byte*)pointer)[address];
            }
            if(step) cpu.PC++;
            cpu.Cycle++;
            return data;
        }
    }

    /// <summary>
    /// Reads a word (ushort) from the given address
    /// </summary>
    /// <param name="cpu"></param>
    /// <param name="address"></param>
    /// <param name="step"></param>
    /// <returns></returns>
    public ushort ReadWord(CPU cpu, ushort address, bool step = true)
    {
        unsafe
        {
            ushort data;
            if (Bus.TryGetValue(address, out var value))
            {
                data = value.ReadWord(cpu, this, address);
            }
            else
            {
                data = ((ushort*)pointer)[address];
            }
            if(step) cpu.PC+=2;
            cpu.Cycle+=2;
            return data;
        }
    }


    /// <summary>
    /// Writes a byte at the given address
    /// </summary>
    /// <param name="cpu"></param>
    /// <param name="address"></param>
    /// <param name="value"></param>
    /// <param name="step"></param>
    public void WriteByte(CPU cpu, ushort address, byte value, bool step = true)
    {
        unsafe
        {
            if (Bus.TryGetValue(address, out var busItem))
            {
                busItem.WriteByte(cpu, this, address, value);
            }
            else
            {
                ((byte*)pointer)[address] = value;
            }
            if(step) cpu.PC++;
            cpu.Cycle++;

        }
    }

    /// <summary>
    /// Writes a word to the given address
    /// </summary>
    /// <param name="cpu"></param>
    /// <param name="address"></param>
    /// <param name="value"></param>
    /// <param name="step"></param>
    public void WriteWord(CPU cpu, ushort address, ushort value, bool step = true)
    {
        unsafe
        {
            if (Bus.TryGetValue(address, out var busItem))
            {
                busItem.WriteWord(cpu, this, address, value);
            }
            else
            {
                ((ushort*)pointer)[address] = value;
            }
            if(step) cpu.PC+=2;
            cpu.Cycle+=2;
        }
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}
