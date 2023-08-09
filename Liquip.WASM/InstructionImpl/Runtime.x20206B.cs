#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // local.local.132.sub
    public static void x20206B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 = state.locals[state.program[state.ip].a].i32 -
                                       state.locals[state.program[state.ip].b].i32;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.132.mul
    public static void x20206C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 = state.locals[state.program[state.ip].a].i32 *
                                       state.locals[state.program[state.ip].b].i32;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.132.div_s
    public static void x20206D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 =
            (uint)((int)state.locals[state.program[state.ip].a].i32 /
                   (int)state.locals[state.program[state.ip].b].i32);
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.132.div_u
    public static void x20206E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 = state.locals[state.program[state.ip].a].i32 /
                                       state.locals[state.program[state.ip].b].i32;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.132.rem_s
    public static void x20206F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 =
            (uint)(state.locals[state.program[state.ip].a].i32 == 0x80000000 &&
                   state.locals[state.program[state.ip].b].i32 == 0xFFFFFFFF
                ? 0
                : (int)state.locals[state.program[state.ip].a].i32 %
                  (int)state.locals[state.program[state.ip].b].i32);
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.132.rem_u
    public static void x202070(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 = state.locals[state.program[state.ip].a].i32 %
                                       state.locals[state.program[state.ip].b].i32;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.and
    public static void x202071(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 = state.locals[state.program[state.ip].a].i32 &
                                       state.locals[state.program[state.ip].b].i32;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.or
    public static void x202072(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 = state.locals[state.program[state.ip].a].i32 |
                                       state.locals[state.program[state.ip].b].i32;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.xor
    public static void x202073(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 = state.locals[state.program[state.ip].a].i32 ^
                                       state.locals[state.program[state.ip].b].i32;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.shl
    public static void x202074(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 = state.locals[state.program[state.ip].a].i32 <<
                                       (int)state.locals[state.program[state.ip].b].i32;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.shr_s
    public static void x202075(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].i32 =
            (uint)((int)state.locals[state.program[state.ip].a].i32 >>
                   (int)state.locals[state.program[state.ip].b].i32);
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.shr_u
    public static void x202076(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].i32 = state.locals[state.program[state.ip].a].i32 >>
                                       (int)state.locals[state.program[state.ip].b].i32;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.f64.add
    public static void x2020A0(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 = state.locals[state.program[state.ip].a].f64 +
                                       state.locals[state.program[state.ip].b].f64;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.f64.sub
    public static void x2020A1(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 = state.locals[state.program[state.ip].a].f64 -
                                       state.locals[state.program[state.ip].b].f64;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.f64.mul
    public static void x2020A2(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 = state.locals[state.program[state.ip].a].f64 *
                                       state.locals[state.program[state.ip].b].f64;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }
}
