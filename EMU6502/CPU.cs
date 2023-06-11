using Zarlo.Cosmos.Utils;
using EMU6502.Interface;

namespace EMU6502;

[Flags]
public enum StatusFlags: byte
{ 
    C = 0b1000_0000, // Carry Flag
    Z = 0b0100_0000, // Zero Flag
    I = 0b0010_0000, // Interrupt disable
    D = 0b0001_0000, // Decimal mode
    B = 0b0000_1000, // Break
    Unused = 0b0000_0100,
    V = 0b0000_0010, // Overflow
    N = 0b0000_0001, // Negative
}

public static class StatusFlagsEx
{
    public static bool CosmosHasFlag(this StatusFlags me, StatusFlags flag) 
    {
        return Has.Flag((byte)me, (byte)flag);
    }
}

public class CPU
{

    public Dictionary<byte, IOpCode> OpCodes = new Dictionary<byte, IOpCode>();

    public UInt16 PC { get; set; }
    public byte SP { get; set; }

    public byte A { get; set; }
    public byte X { get; set; }
    public byte Y { get; set; }


    StatusFlags Flag { get; set; }

    public UInt16 Cycle { get; set; }

    public AddressSpace AddressSpace { get; set; }

    public void Interrupt()
    {
        if (Flag.HasFlag(StatusFlags.I))
        { 

        }
    }

    public void Tick()
    {
        OpCodes[AddressSpace.ReadByte(this, PC)].Execute(this, AddressSpace);
    }


}
