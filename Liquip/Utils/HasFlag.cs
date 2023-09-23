using System.Runtime.CompilerServices;

namespace Liquip.Utils;

/// <summary>
/// has utils
/// </summary>
public static class Has
{
    /// <summary>
    /// flag
    /// </summary>
    /// <param name="value"></param>
    /// <param name="bit"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(uint value, int bit)
    {
        return ((value << bit) & 1) == 1;
    }

    /// <summary>
    /// flag
    /// </summary>
    /// <param name="value"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(byte value, byte flag)
    {
        return (value & flag) == 1;
    }

    /// <summary>
    /// flag
    /// </summary>
    /// <param name="value"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(int value, int flag)
    {
        return (value & flag) == 1;
    }

    /// <summary>
    /// flag
    /// </summary>
    /// <param name="value"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(long value, long flag)
    {
        return (value & flag) == 1;
    }
}

/// <summary>
/// set utils
/// </summary>
public static class Set
{
    /// <summary>
    /// set flag
    /// </summary>
    /// <param name="value"></param>
    /// <param name="bit"></param>
    /// <param name="set"></param>
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

    /// <summary>
    /// set flag
    /// </summary>
    /// <param name="value"></param>
    /// <param name="bit"></param>
    /// <param name="set"></param>
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

    /// <summary>
    /// set flag
    /// </summary>
    /// <param name="value"></param>
    /// <param name="bit"></param>
    /// <param name="set"></param>
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
