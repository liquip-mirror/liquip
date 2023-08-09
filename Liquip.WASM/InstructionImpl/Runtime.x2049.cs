#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // local.i32.lt_u
    public static void x2049(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            vStack[state.vStackPtr].i32 < state.locals[state.program[state.ip].a].i32 ? 1 : (uint)0;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.gt_s
    public static void x204A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 = (int)vStack[state.vStackPtr].i32 >
                                       (int)state.locals[state.program[state.ip].a].i32
            ? 1
            : (uint)0;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.gt_u
    public static void x204B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            vStack[state.vStackPtr].i32 > state.locals[state.program[state.ip].a].i32 ? 1 : (uint)0;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.add
    public static void x206A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            state.locals[state.program[state.ip].a].i32 + vStack[state.vStackPtr].i32;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.sub
    public static void x206B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            vStack[state.vStackPtr].i32 - state.locals[state.program[state.ip].a].i32;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.mul
    public static void x206C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            state.locals[state.program[state.ip].a].i32 * vStack[state.vStackPtr].i32;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.div_s
    public static void x206D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            (uint)((int)vStack[state.vStackPtr].i32 / (int)state.locals[state.program[state.ip].a].i32);
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.div_u
    public static void x206E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            vStack[state.vStackPtr].i32 / state.locals[state.program[state.ip].a].i32;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.rem_s
    public static void x206F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            (uint)((vStack[state.vStackPtr].i32 == 0x80000000) &
                   (state.locals[state.program[state.ip].a].i32 == 0xFFFFFFFF)
                ? 0
                : (int)vStack[state.vStackPtr].i32 % (int)state.locals[state.program[state.ip].a].i32);
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.rem_u
    public static void x2070(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            vStack[state.vStackPtr].i32 % state.locals[state.program[state.ip].a].i32;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.and
    public static void x2071(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            state.locals[state.program[state.ip].a].i32 & vStack[state.vStackPtr].i32;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.or
    public static void x2072(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            state.locals[state.program[state.ip].a].i32 | vStack[state.vStackPtr].i32;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.xor
    public static void x2073(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            state.locals[state.program[state.ip].a].i32 ^ vStack[state.vStackPtr].i32;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.shl
    public static void x2074(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 = vStack[state.vStackPtr].i32 <<
                                       (int)state.locals[state.program[state.ip].a].i32;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.shr_s
    public static void x2075(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            (uint)((int)vStack[state.vStackPtr].i32 >>
                   (int)state.locals[state.program[state.ip].a].i32);
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.shr_u
    public static void x2076(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 = vStack[state.vStackPtr].i32 >>
                                       (int)state.locals[state.program[state.ip].a].i32;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.abs
    public static void x2099(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 = Math.Abs(state.locals[state.program[state.ip].a].f64);
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.neg
    public static void x209A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 = -state.locals[state.program[state.ip].a].f64;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.ceil
    public static void x209B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 = Math.Ceiling(state.locals[state.program[state.ip].a].f64);
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.floor
    public static void x209C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 = Math.Floor(state.locals[state.program[state.ip].a].f64);
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.trunc
    public static void x209D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 = Math.Truncate(state.locals[state.program[state.ip].a].f64);
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.nearest
    public static void x209E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 = Math.Round(state.locals[state.program[state.ip].a].f64);
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.sqrt
    public static void x209F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 = Math.Sqrt(state.locals[state.program[state.ip].a].f64);
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.add
    public static void x20A0(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].f64 =
            vStack[state.vStackPtr].f64 + state.locals[state.program[state.ip].a].f64;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.sub
    public static void x20A1(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].f64 =
            vStack[state.vStackPtr].f64 - state.locals[state.program[state.ip].a].f64;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.mul
    public static void x20A2(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].f64 =
            vStack[state.vStackPtr].f64 * state.locals[state.program[state.ip].a].f64;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.div
    public static void x20A3(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].f64 =
            vStack[state.vStackPtr].f64 / state.locals[state.program[state.ip].a].f64;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.min
    public static void x20A4(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].f64 = Math.Min(vStack[state.vStackPtr].f64,
            state.locals[state.program[state.ip].a].f64);
        ++state.vStackPtr;
        ++state.ip;
    }
}
