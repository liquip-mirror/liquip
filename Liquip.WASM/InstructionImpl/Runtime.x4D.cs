#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All

using Liquip.WASM.Instruction;

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // i32.le_u
    public static void x4D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 <=
            vStack[state.vStackPtr - 1].i32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i32.ge_s
    public static void x4E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            (int)vStack[state.vStackPtr - 2].i32 >=
            (int)vStack[state.vStackPtr - 1].i32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i32.ge_u
    public static void x4F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 >=
            vStack[state.vStackPtr - 1].i32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i64.eqz
    public static void x50(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i32;
        vStack[state.vStackPtr - 1].i32 =
            vStack[state.vStackPtr - 1].i64 == 0 ? 1 : (uint)0;
    }

    // i64.eq
    public static void x51(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i64 ==
            vStack[state.vStackPtr - 1].i64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i64.ne
    public static void x52(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i64 !=
            vStack[state.vStackPtr - 1].i64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i64.lt_s
    public static void x53(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            (long)vStack[state.vStackPtr - 2].i64 <
            (long)vStack[state.vStackPtr - 1].i64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i64.lt_u
    public static void x54(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i64 <
            vStack[state.vStackPtr - 1].i64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i64.gt_s
    public static void x55(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            (long)vStack[state.vStackPtr - 2].i64 >
            (long)vStack[state.vStackPtr - 1].i64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i64.gt_u
    public static void x56(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i64 >
            vStack[state.vStackPtr - 1].i64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i64.le_s
    public static void x57(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            (long)vStack[state.vStackPtr - 2].i64 <=
            (long)vStack[state.vStackPtr - 1].i64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i64.le_u
    public static void x58(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i64 <=
            vStack[state.vStackPtr - 1].i64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i64.ge_s
    public static void x59(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            (long)vStack[state.vStackPtr - 2].i64 >=
            (long)vStack[state.vStackPtr - 1].i64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i64.ge_u
    public static void x5A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i64 >=
            vStack[state.vStackPtr - 1].i64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f32.eq
    public static void x5B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].f32.Equals(
                vStack[state.vStackPtr - 1].f32)
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f32.ne
    public static void x5C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            !vStack[state.vStackPtr - 2].f32.Equals(
                vStack[state.vStackPtr - 1].f32)
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f32.lt
    public static void x5D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].f32 <
            vStack[state.vStackPtr - 1].f32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f32.gt
    public static void x5E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].f32 >
            vStack[state.vStackPtr - 1].f32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f32.le
    public static void x5F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].f32 <=
            vStack[state.vStackPtr - 1].f32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f32.ge
    public static void x60(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].f32 >=
            vStack[state.vStackPtr - 1].f32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f64.eq
    public static void x61(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].f64.Equals(vStack[state.vStackPtr - 1].f64)

                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f64.ne
    public static void x62(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].f64 !=
            vStack[state.vStackPtr - 1].f64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f64.lt
    public static void x63(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].f64 <
            vStack[state.vStackPtr - 1].f64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f64.gt
    public static void x64(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].f64 >
            vStack[state.vStackPtr - 1].f64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f64.le
    public static void x65(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].f64 <=
            vStack[state.vStackPtr - 1].f64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // f64.ge
    public static void x66(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].type = Type.i32;
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].f64 >=
            vStack[state.vStackPtr - 1].f64
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }
}
