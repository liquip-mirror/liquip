#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All


using Liquip.WASM.Instruction;

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // i64.clz
    public static void x79(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        // TODO: optimize this
        var a = vStack[state.vStackPtr - 1].i64;

        uint bits = 0;
        var compare = 0x8000000000000000;
        while (bits < 64)
        {
            if ((compare & a) == 0)
            {
                bits++;
                compare >>= 1;
            }
            else
            {
                break;
            }
        }

        vStack[state.vStackPtr - 1].i64 = bits;
    }

    // i64.ctz
    public static void x7A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        // TODO: optimize this
        var a = vStack[state.vStackPtr - 1].i64;

        ulong bits = 0;
        ulong compare = 1;
        while (bits < 64)
        {
            if ((compare & a) == 0)
            {
                bits++;
                compare <<= 1;
            }
            else
            {
                break;
            }
        }

        vStack[state.vStackPtr - 1].i64 = bits;
    }

    // i64.popcnt
    public static void x7B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        // TODO: optimize this
        var a = vStack[state.vStackPtr - 1].i64;

        ulong bits = 0;
        ulong compare = 1;
        while (true)
        {
            if ((compare & a) != 0)
            {
                bits++;
            }

            if (compare == 0x8000000000000000)
            {
                break;
            }

            compare <<= 1;
        }

        vStack[state.vStackPtr - 1].i64 = bits;
    }

    // i64.add
    public static void x7C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            vStack[state.vStackPtr - 2].i64 +
            vStack[state.vStackPtr - 1].i64;
        --state.vStackPtr;
    }

    // i64.sub
    public static void x7D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            vStack[state.vStackPtr - 2].i64 -
            vStack[state.vStackPtr - 1].i64;
        --state.vStackPtr;
    }

    // i64.mul
    public static void x7E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            vStack[state.vStackPtr - 2].i64 *
            vStack[state.vStackPtr - 1].i64;
        --state.vStackPtr;
    }

    // i64.div_s
    public static void x7F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            (ulong)((long)vStack[state.vStackPtr - 2].i64 /
                    (long)vStack[state.vStackPtr - 1].i64);
        --state.vStackPtr;
    }

    // i64.div_u
    public static void x80(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            vStack[state.vStackPtr - 2].i64 /
            vStack[state.vStackPtr - 1].i64;
        --state.vStackPtr;
    }

    // i64.rem_s
    public static void x81(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            (ulong)(vStack[state.vStackPtr - 2].i64 == 0x8000000000000000 &&
                    vStack[state.vStackPtr - 1].i64 == 0xFFFFFFFFFFFFFFFF
                ? 0
                : (long)vStack[state.vStackPtr - 2].i64 %
                  (long)vStack[state.vStackPtr - 1].i64);
        --state.vStackPtr;
    }

    // i64.rem_u
    public static void x82(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            vStack[state.vStackPtr - 2].i64 %
            vStack[state.vStackPtr - 1].i64;
        --state.vStackPtr;
    }

    // i64.and
    public static void x83(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            vStack[state.vStackPtr - 2].i64 &
            vStack[state.vStackPtr - 1].i64;
        --state.vStackPtr;
    }

    // i64.or
    public static void x84(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            vStack[state.vStackPtr - 2].i64 |
            vStack[state.vStackPtr - 1].i64;
        --state.vStackPtr;
    }

    // i64.xor
    public static void x85(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            vStack[state.vStackPtr - 2].i64 ^
            vStack[state.vStackPtr - 1].i64;
        --state.vStackPtr;
    }

    // i64.shl
    public static void x86(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            vStack[state.vStackPtr - 2].i64 <<
            (int)vStack[state.vStackPtr - 1].i64;
        --state.vStackPtr;
    }

    // i64.shr_s
    public static void x87(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            (ulong)((long)vStack[state.vStackPtr - 2].i64 >>
                    (int)vStack[state.vStackPtr - 1].i64);
        --state.vStackPtr;
    }

    // i64.shr_u
    public static void x88(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            vStack[state.vStackPtr - 2].i64 >>
            (int)vStack[state.vStackPtr - 1].i64;
        --state.vStackPtr;
    }

    // i64.rotl
    public static void x89(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            (vStack[state.vStackPtr - 2].i64 << (int)vStack[state.vStackPtr - 1].i64) |
            (vStack[state.vStackPtr - 2].i64 >>
             (64 - (int)vStack[state.vStackPtr - 1].i64));
        --state.vStackPtr;
    }

    // i64.rotr
    public static void x8A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i64 =
            (vStack[state.vStackPtr - 2].i64 >>
             (int)vStack[state.vStackPtr - 1].i64) |
            (vStack[state.vStackPtr - 2].i64 <<
             (64 - (int)vStack[state.vStackPtr - 1].i64));
        --state.vStackPtr;
    }

}
