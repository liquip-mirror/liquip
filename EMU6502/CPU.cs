using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using EMU6502.Interface;
// ReSharper disable ArrangeTypeMemberModifiers

namespace EMU6502;

public class StatusFlagsOffSets
{
    public const byte C = 1, // Carry Flag
        Z = 2, // Zero Flag
        I = 3, // Interrupt disable
        D = 4, // Decimal mode
        B = 5, // Break
        Unused = 6,
        V = 7, // Overflow
        N = 8; // Negative
}

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct StatusFlag
{
    /// <summary>
    /// the raw byte
    /// </summary>
    public byte Raw;

    /// <summary>
    /// Carry Flag
    /// </summary>
    public bool C {
        get => Utils.Has.Flag(Raw, StatusFlagsOffSets.C);
        set => Utils.Set.Flag(ref Raw, StatusFlagsOffSets.C, value);
    }

    /// <summary>
    /// Zero Flag
    /// </summary>
    public bool Z {
        get => Utils.Has.Flag(Raw, StatusFlagsOffSets.Z);
        set => Utils.Set.Flag(ref Raw, StatusFlagsOffSets.Z, value);
    }

    /// <summary>
    /// Interrupt disable
    /// </summary>
    public bool I {
        get => Utils.Has.Flag(Raw, StatusFlagsOffSets.I);
        set => Utils.Set.Flag(ref Raw, StatusFlagsOffSets.I, value);
    }

    /// <summary>
    /// Decimal mode
    /// </summary>
    public bool D {
        get => Utils.Has.Flag(Raw, StatusFlagsOffSets.D);
        set => Utils.Set.Flag(ref Raw, StatusFlagsOffSets.D, value);
    }

    /// <summary>
    /// Break
    /// </summary>
    public bool B {
        get => Utils.Has.Flag(Raw, StatusFlagsOffSets.B);
        set => Utils.Set.Flag(ref Raw, StatusFlagsOffSets.B, value);
    }

    /// <summary>
    /// Unused
    /// </summary>
    public bool Unused {
        get => Utils.Has.Flag(Raw, StatusFlagsOffSets.Unused);
        set => Utils.Set.Flag(ref Raw, StatusFlagsOffSets.Unused, value);
    }

    /// <summary>
    /// Overflow
    /// </summary>
    public bool V {
        get => Utils.Has.Flag(Raw, StatusFlagsOffSets.V);
        set => Utils.Set.Flag(ref Raw, StatusFlagsOffSets.V, value);
    }

    /// <summary>
    /// Negative
    /// </summary>
    public bool N {
        get => Utils.Has.Flag(Raw, StatusFlagsOffSets.N);
        set => Utils.Set.Flag(ref Raw, StatusFlagsOffSets.N, value);
    }
}

/// <summary>
/// 6502 CPU
/// </summary>
public class CPU
{

    /// <summary>
    /// CPU OPCODES
    /// </summary>
    public static readonly ReadOnlyDictionary<byte, IOpCode> OpCodes =
        new ReadOnlyDictionary<byte, IOpCode>(new Dictionary<byte, IOpCode>()
    {

    });


    /// <summary>
    /// program counter
    /// </summary>
    public ushort PC { get; set; }

    /// <summary>
    /// stack pointer
    /// </summary>
    public byte SP { get; set; }

    /// <summary>
    /// register
    /// </summary>
    public byte A    {
        get => _a;
        set
        {
            _a = value;
            SetZeroAndNegativeFlags(value);
        }
    }


    /// <summary>
    /// register
    /// </summary>
    public byte X    {
        get => _x;
        set
        {
            _x = value;
            SetZeroAndNegativeFlags(value);
        }
    }


    /// <summary>
    /// register
    /// </summary>
    public byte Y
    {
        get => _y;
        set
        {
            _y = value;
            SetZeroAndNegativeFlags(value);
        }
    }

    void SetZeroAndNegativeFlags(byte value)
    {
        Flag.Z = (value == 0);
        Flag.N = (sbyte)value < 0;
    }

    private StatusFlag _flag = new StatusFlag();

    private byte _y = 0;
    private byte _x = 0;
    private byte _a = 0;

    /// <summary>
    /// cpu flags
    /// </summary>
    public ref StatusFlag Flag => ref _flag;

    /// <summary>
    /// the current CPU cycle
    /// </summary>
    public ushort Cycle { get; set; } = 0;

    /// <summary>
    /// Cpu Address space
    /// </summary>
    public AddressSpace AddressSpace { get; init; }


    /// <summary>
    /// 6502 with just RAM
    /// </summary>
    public CPU()
    {
        AddressSpace = new AddressSpace();
    }

    /// <summary>
    /// 6502 with custom Address space
    /// </summary>
    /// <param name="addressSpace"></param>
    public CPU(AddressSpace addressSpace)
    {
        AddressSpace = addressSpace;
    }

    /// <summary>
    /// do a CPU Interrupt
    /// </summary>
    public void Interrupt()
    {
        if (Flag.I)
        {

        }
    }

    /// <summary>
    /// Auto ticks
    /// </summary>
    /// <param name="cycles">the amount of cycles to tick</param>
    public void TickUntil(ushort cycles)
    {

        cycles += Cycle;

        while (cycles > Cycle)
        {
            OpCodes[AddressSpace.ReadByte(this, PC)].Execute( this, AddressSpace);
        }
    }

    /// <summary>
    /// tick the CPU
    /// </summary>
    public void Tick()
    {
        OpCodes[AddressSpace.ReadByte(this, PC)].Execute( this, AddressSpace);
    }

    /// <summary>
    /// get the address of the SP
    /// </summary>
    /// <returns>address of the SP</returns>
    public ushort SpToAddress()
    {
        return (ushort)(0x100 | SP);
    }

    /// <summary>
    /// resets the cpu and clears RAM
    /// </summary>
    /// <param name="resetVector"></param>
    public void Reset(ushort resetVector = 0xFFFC)
    {
        PC = resetVector;
        SP = 0xFF;
        Flag.Raw = 0x00;
        A = X = Y = 0;
    }

    /// <summary>
    /// push a ushort to the stack
    /// </summary>
    /// <param name="value"></param>
    public void PushWordToStack(ushort value)
    {
        SP--;
        SP--;
        AddressSpace.WriteWord(this, SpToAddress(), value);
    }

    /// <summary>
    /// push byte to stack
    /// </summary>
    /// <param name="value"></param>
    public void PushByteToStack(byte value)
    {
        SP--;
        AddressSpace.WriteByte(this, SpToAddress(), value);
    }

    /// <summary>
    /// pop ushort off stack
    /// </summary>
    /// <returns></returns>
    public ushort PopWordToStack()
    {
        SP++;
        SP++;
        return AddressSpace.ReadWord(this, SpToAddress());
    }

    /// <summary>
    /// pop byte off stack
    /// </summary>
    /// <returns></returns>
    public byte PopByteToStack()
    {
        SP++;
        return AddressSpace.ReadByte(this, SpToAddress());
    }

}
