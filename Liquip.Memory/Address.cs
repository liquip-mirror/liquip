using System.Runtime.InteropServices;

namespace Liquip.Memory;


/// <summary>
/// a struct that holds an Address
/// </summary>
#if IS64BIT
[StructLayout(LayoutKind.Explicit, Size = 8)]
#else
[StructLayout(LayoutKind.Explicit, Size = 4)]
#endif
public readonly struct Address
{
#if IS64BIT
    [FieldOffset(0)]
    private UInt64 _value;
    public Address(UInt64 value)
    {
        _value = value;
    }

    public static explicit operator Address(UInt64 value) {
        return new Address(value);
    }

    public static implicit operator UInt64(Address me) {
        return me._value;
    }

    public bool Is64Bit => true;
    public bool Is32Bit => false;

#else
    [FieldOffset(0)]
    private readonly uint _value;

    public bool Is64Bit => false;
    public bool Is32Bit => true;

    public Address(uint value)
    {
        _value = value;
    }

    /// <summary>
    /// cast uint ot address
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static explicit operator Address(uint value)
    {
        return new Address(value);
    }

    /// <summary>
    /// cast a Address to a uint
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public static implicit operator uint(Address me)
    {
        return me._value;
    }

    /// <summary>
    /// cast a Address to a int
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public static implicit operator int(Address me)
    {
        return (int)me._value;
    }

#endif

    /// <summary>
    ///
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Address operator +(Address a, Address b)
    {
        return new Address(a._value + b._value);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Address operator -(Address a, Address b)
    {
        return new Address(a._value - b._value);
    }

    /// <summary>
    /// Address
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return (int)_value;
    }
}
