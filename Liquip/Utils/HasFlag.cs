using System.Runtime.CompilerServices;

namespace Liquip.Utils;

public static class Has
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(uint value, int bit)
    {
        return ((value << bit) & 1) == 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(byte value, byte flag)
    {
        return (value & flag) == 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(int value, int flag)
    {
        return (value & flag) == 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(long value, long flag)
    {
        return (value & flag) == 1;
    }
}
