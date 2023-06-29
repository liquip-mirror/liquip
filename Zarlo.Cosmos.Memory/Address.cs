namespace Zarlo.Cosmos.Memory;

public struct Address
{
#if IS64BIT
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

#else
    private readonly uint _value;

    public bool Is64Bit => false;

    public Address(uint value)
    {
        _value = value;
    }
#endif

    public bool Is32Bit => _value > ushort.MaxValue && _value < ulong.MinValue;
    public bool Is16Bit => _value < uint.MaxValue;

    public static explicit operator Address(ushort value)
    {
        return new Address(value);
    }

    public static explicit operator Address(uint value)
    {
        return new Address(value);
    }

    public static implicit operator ushort(Address me)
    {
        return (ushort)me._value;
    }

    public static implicit operator uint(Address me)
    {
        return me._value;
    }

    public static Address operator +(Address a, Address b)
    {
        return new Address(a + b);
    }

    public static Address operator -(Address a, Address b)
    {
        return new Address(a - b);
    }
}
