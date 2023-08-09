#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All


using Liquip.WASM.Instruction;

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // f32.abs
    public static void x8B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f32 =
            Math.Abs(vStack[state.vStackPtr - 1].f32);
    }

    // f32.neg
    public static void x8C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f32 = -vStack[state.vStackPtr - 1].f32;
    }

    // f32.ceil
    public static void x8D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f32 =
            (float)Math.Ceiling(vStack[state.vStackPtr - 1].f32);
    }

    // f32.floor
    public static void x8E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f32 =
            (float)Math.Floor(vStack[state.vStackPtr - 1].f32);
    }

    // f32.trunc
    public static void x8F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f32 =
            (float)Math.Truncate(vStack[state.vStackPtr - 1].f32);
    }

    // f32.nearest
    public static void x90(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f32 =
            (float)Math.Round(vStack[state.vStackPtr - 1].f32);
    }

    // f32.sqrt
    public static void x91(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f32 =
            (float)Math.Sqrt(vStack[state.vStackPtr - 1].f32);
    }

    // f32.add
    public static void x92(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f32 =
            vStack[state.vStackPtr - 2].f32 +
            vStack[state.vStackPtr - 1].f32;
        --state.vStackPtr;
    }

    // f32.sub
    public static void x93(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f32 =
            vStack[state.vStackPtr - 2].f32 -
            vStack[state.vStackPtr - 1].f32;
        --state.vStackPtr;
    }

    // f32.mul
    public static void x94(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f32 =
            vStack[state.vStackPtr - 2].f32 *
            vStack[state.vStackPtr - 1].f32;
        --state.vStackPtr;
    }

    // f32.div
    public static void x95(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f32 =
            vStack[state.vStackPtr - 2].f32 /
            vStack[state.vStackPtr - 1].f32;
        --state.vStackPtr;
    }

    // f32.min
    public static void x96(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f32 = Math.Min(
            vStack[state.vStackPtr - 2].f32,
            vStack[state.vStackPtr - 1].f32);
        --state.vStackPtr;
    }

    // f32.max
    public static void x97(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f32 = Math.Max(
            vStack[state.vStackPtr - 2].f32,
            vStack[state.vStackPtr - 1].f32);
        --state.vStackPtr;
    }

    // f32.copysign
    public static void x98(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        if (vStack[state.vStackPtr - 2].f32 >= 0 &&
            vStack[state.vStackPtr - 1].f32 < 0)
        {
            vStack[state.vStackPtr - 1].f32 =
                -vStack[state.vStackPtr - 1].f32;
        }

        if (vStack[state.vStackPtr - 2].f32 < 0 &&
            vStack[state.vStackPtr - 1].f32 >= 0)
        {
            vStack[state.vStackPtr - 1].f32 =
                -vStack[state.vStackPtr - 1].f32;
        }

        --state.vStackPtr;
    }

    // f64.abs
    public static void x99(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f64 =
            Math.Abs(vStack[state.vStackPtr - 1].f64);
    }

    // f64.neg
    public static void x9A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f64 = -vStack[state.vStackPtr - 1].f64;
    }

    // f64.ceil
    public static void x9B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f64 =
            Math.Ceiling(vStack[state.vStackPtr - 1].f64);
    }

    // f64.floor
    public static void x9C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f64 =
            Math.Floor(vStack[state.vStackPtr - 1].f64);
    }

    // f64.trunc
    public static void x9D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f64 =
            Math.Truncate(vStack[state.vStackPtr - 1].f64);
    }

    // f64.nearest
    public static void x9E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f64 =
            Math.Round(vStack[state.vStackPtr - 1].f64);
    }

    // f64.sqrt
    public static void x9F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f64 =
            Math.Sqrt(vStack[state.vStackPtr - 1].f64);
    }

    // f64.add
    public static void xA0(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f64 =
            vStack[state.vStackPtr - 2].f64 +
            vStack[state.vStackPtr - 1].f64;
        --state.vStackPtr;
    }

    // f64.sub
    public static void xA1(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f64 =
            vStack[state.vStackPtr - 2].f64 -
            vStack[state.vStackPtr - 1].f64;
        --state.vStackPtr;
    }

    // f64.mul
    public static void xA2(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f64 =
            vStack[state.vStackPtr - 2].f64 *
            vStack[state.vStackPtr - 1].f64;
        --state.vStackPtr;
    }

    // f64.div
    public static void xA3(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f64 =
            vStack[state.vStackPtr - 2].f64 /
            vStack[state.vStackPtr - 1].f64;
        --state.vStackPtr;
    }

    // f64.min
    public static void xA4(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f64 = Math.Min(
            vStack[state.vStackPtr - 2].f64,
            vStack[state.vStackPtr - 1].f64);
        --state.vStackPtr;
    }

    // f64.max
    public static void xA5(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].f64 = Math.Max(
            vStack[state.vStackPtr - 2].f64,
            vStack[state.vStackPtr - 1].f64);
        --state.vStackPtr;
    }

    // f64.copysign
    public static void xA6(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        if (vStack[state.vStackPtr - 2].f64 >= 0 &&
            vStack[state.vStackPtr - 1].f64 < 0)
        {
            vStack[state.vStackPtr - 1].f64 =
                -vStack[state.vStackPtr - 1].f64;
        }

        if (vStack[state.vStackPtr - 2].f64 < 0 &&
            vStack[state.vStackPtr - 1].f64 >= 0)
        {
            vStack[state.vStackPtr - 1].f64 =
                -vStack[state.vStackPtr - 1].f64;
        }

        --state.vStackPtr;
    }

    // i32.wrap_i64
    public static void xA7(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i32;
        vStack[state.vStackPtr - 1].i32 =
            (uint)vStack[state.vStackPtr - 1].i64;
    }

    // i32.trunc_f32_s
    public static void xA8(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i32;
        vStack[state.vStackPtr - 1].i32 =
            (uint)(int)Math.Truncate(vStack[state.vStackPtr - 1].f32);
    }

    // i32.trunc_f32_u
    public static void xA9(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i32;
        vStack[state.vStackPtr - 1].i32 =
            (uint)Math.Truncate(vStack[state.vStackPtr - 1].f32);
    }

    // i32.trunc_f64_s
    public static void xAA(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i32;
        vStack[state.vStackPtr - 1].i32 =
            (uint)(int)Math.Truncate(vStack[state.vStackPtr - 1].f64);
    }

    // i32.trunc_f64_u
    public static void xAB(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i32;
        vStack[state.vStackPtr - 1].i32 =
            (uint)Math.Truncate(vStack[state.vStackPtr - 1].f64);
    }

    // i64.extend_i32_s
    public static void xAC(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i64;
        vStack[state.vStackPtr - 1].i64 =
            (ulong)(int)vStack[state.vStackPtr - 1].i32;
    }

    // i64.extend_i32_u
    public static void xAD(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i64;
        vStack[state.vStackPtr - 1].i64 =
            vStack[state.vStackPtr - 1].i32;
    }

    // i64.trunc_f32_s
    public static void xAE(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i64;
        vStack[state.vStackPtr - 1].i64 =
            (ulong)(long)Math.Truncate(vStack[state.vStackPtr - 1].f32);
    }
}
