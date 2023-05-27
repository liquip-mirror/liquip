using System.Runtime.CompilerServices;

namespace Cosmos.Zarlo.Utils;


public static partial class Has
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(uint value, int bit) => (((value << bit) & 1) == 1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(byte value, byte flag) => ((value & flag) == 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(int value, int flag) => ((value & flag) == 1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Flag(long value, long flag) => ((value & flag) == 1);

}
