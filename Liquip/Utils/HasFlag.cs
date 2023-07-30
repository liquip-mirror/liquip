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

public static class Set
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Flag(ref uint value, int bit, bool set = true)
    {
        if (set)
        {
            value |= ((uint)1 << bit);
        }
        else
        {
            value &= ~((uint)1 << bit);

        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Flag(ref int value, int bit, bool set = true)
    {
        if (set)
        {
            value |= (1 << bit);
        }
        else
        {
            value &= ~(1 << bit);

        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Flag(ref long value, int bit, bool set = true)
    {
        if (set)
        {
            value |= ((long)1 << bit);
        }
        else
        {
            value &= ~((long)1 << bit);

        }
    }
}
