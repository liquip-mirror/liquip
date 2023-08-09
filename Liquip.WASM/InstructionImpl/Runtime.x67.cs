#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All


using Liquip.WASM.Instruction;

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // i32.clz
    public static void x67(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        // TODO: optimize this
        var a = vStack[state.vStackPtr - 1].i32;

        uint bits = 0;
        var compare = 0x80000000;
        while (bits < 32)
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

        vStack[state.vStackPtr - 1].i32 = bits;
    }

    // i32.ctz
    public static void x68(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        // TODO: optimize this
        var a = vStack[state.vStackPtr - 1].i32;

        uint bits = 0;
        uint compare = 1;
        while (bits < 32)
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

        vStack[state.vStackPtr - 1].i32 = bits;
    }

    // i32.popcnt
    public static void x69(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        // TODO: optimize this
        var a = vStack[state.vStackPtr - 1].i32;

        uint bits = 0;
        uint compare = 1;
        while (true)
        {
            if ((compare & a) != 0)
            {
                bits++;
            }

            if (compare == 0x80000000)
            {
                break;
            }

            compare <<= 1;
        }

        vStack[state.vStackPtr - 1].i32 = bits;
    }

    // i32.add
    public static void x6A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 +
            vStack[state.vStackPtr - 1].i32;
        --state.vStackPtr;
    }

    // i32.sub
    public static void x6B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 -
            vStack[state.vStackPtr - 1].i32;
        --state.vStackPtr;
    }

    // i32.mul
    public static void x6C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 *
            vStack[state.vStackPtr - 1].i32;
        --state.vStackPtr;
    }

    // i32.div_s
    public static void x6D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            (uint)((int)vStack[state.vStackPtr - 2].i32 /
                   (int)vStack[state.vStackPtr - 1].i32);
        --state.vStackPtr;
    }

    // i32.div_u
    public static void x6E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 /
            vStack[state.vStackPtr - 1].i32;
        --state.vStackPtr;
    }

    // i32.rem_s
    public static void x6F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            (uint)(vStack[state.vStackPtr - 2].i32 == 0x80000000 &&
                   vStack[state.vStackPtr - 1].i32 == 0xFFFFFFFF
                ? 0
                : (int)vStack[state.vStackPtr - 2].i32 %
                  (int)vStack[state.vStackPtr - 1].i32);
        --state.vStackPtr;
    }

    // i32.rem_u
    public static void x70(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 %
            vStack[state.vStackPtr - 1].i32;
        --state.vStackPtr;
    }

    // i32.and
    public static void x71(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 &
            vStack[state.vStackPtr - 1].i32;
        --state.vStackPtr;
    }

    // i32.or
    public static void x72(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 |
            vStack[state.vStackPtr - 1].i32;
        --state.vStackPtr;
    }

    // i32.xor
    public static void x73(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 ^
            vStack[state.vStackPtr - 1].i32;
        --state.vStackPtr;
    }

    // i32.shl
    public static void x74(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 <<
            (int)vStack[state.vStackPtr - 1].i32;
        --state.vStackPtr;
    }

    // i32.shr_s
    public static void x75(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            (uint)((int)vStack[state.vStackPtr - 2].i32 >>
                   (int)vStack[state.vStackPtr - 1].i32);
        --state.vStackPtr;
    }

    // i32.shr_u
    public static void x76(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 >>
            (int)vStack[state.vStackPtr - 1].i32;
        --state.vStackPtr;
    }

    // i32.rotl
    public static void x77(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            (vStack[state.vStackPtr - 2].i32 << (int)vStack[state.vStackPtr - 1].i32) |
            (vStack[state.vStackPtr - 2].i32 >>
             (32 - (int)vStack[state.vStackPtr - 1].i32));
        --state.vStackPtr;
    }

    // i32.rotr
    public static void x78(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            (vStack[state.vStackPtr - 2].i32 >> (int)vStack[state.vStackPtr - 1].i32) |
            (vStack[state.vStackPtr - 2].i32 <<
             (32 - (int)vStack[state.vStackPtr - 1].i32));
        --state.vStackPtr;
    }
}
