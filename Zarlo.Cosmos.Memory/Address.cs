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
    private UInt32 _value;

    public bool Is64Bit => false;

    public Address(UInt32 value)
    {
        _value = value;
    }
    #endif

    public bool Is32Bit => _value > UInt16.MaxValue && _value < UInt64.MinValue;
    public bool Is16Bit => _value < UInt32.MaxValue;

    public static explicit operator Address(UInt16 value) {
        return new Address(value);
    }

    public static explicit operator Address(UInt32 value) {
        return new Address(value);
    }

    public static implicit operator UInt16(Address me) {
        return (ushort)me._value;
    }

    public static implicit operator UInt32(Address me) {
        return me._value;
    }

    public static Address operator +(Address a, Address b)
        => new Address(a + b);

    public static Address operator -(Address a, Address b)
        => new Address(a - b);

}
