#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // local.f64.max
    public static void x20A5(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].f64 = Math.Max(vStack[state.vStackPtr].f64,
            state.locals[state.program[state.ip].a].f64);
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.copysign
    public static void x20A6(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        if (vStack[state.vStackPtr].f64 >= 0 && state.locals[state.program[state.ip].a].f64 < 0)
        {
            vStack[state.vStackPtr].f64 = -state.locals[state.program[state.ip].a].f64;
        }

        if (vStack[state.vStackPtr].f64 < 0 && state.locals[state.program[state.ip].a].f64 >= 0)
        {
            vStack[state.vStackPtr].f64 = -state.locals[state.program[state.ip].a].f64;
        }

        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.convert_i32_s
    public static void x20B7(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.f64;
        i = (int)state.locals[state.program[state.ip].a].i32;
        vStack[state.vStackPtr].f64 = i;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.f64.convert_i32_u
    public static void x20B8(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.f64;
        vStack[state.vStackPtr].f64 = state.locals[state.program[state.ip].a].i32;
        ++state.vStackPtr;
        ++state.ip;
    }

    // f64.convert_i32_s.local
    public static void xB721(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        state.locals[state.program[state.ip].a].type = Type.f64;
        i = (int)vStack[state.vStackPtr].i32;
        state.locals[state.program[state.ip].a].f64 = i;
        ++state.ip;
    }

    // f64.convert_i32_u.local
    public static void xB821(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        state.locals[state.program[state.ip].a].type = Type.f64;
        state.locals[state.program[state.ip].a].f64 = vStack[state.vStackPtr].i32;
        ++state.ip;
    }

    // i32.const.local
    public static void x4121(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].a].i32 = state.program[state.ip].i32;
        ++state.ip;
    }

    // i64.const.local
    public static void x4221(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].a].i64 = state.program[state.ip].i64;
        ++state.ip;
    }

    // local.local.i32.store
    public static void x202036(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
        => x202038(ref functions, ref cStackPtr, ref state, ref cStack, ref vStack, ref label, ref length, ref index, ref offset, ref i);

    // local.local.f32.store
    public static void x202038(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b0;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b1;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b2;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b3;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.store
    public static void x202037(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
        => x202039(ref functions, ref cStackPtr, ref state, ref cStack, ref vStack, ref label, ref length, ref index, ref offset, ref i);
    // local.local.f64.store
    public static void x202039(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b0;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b1;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b2;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b3;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b4;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b5;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b6;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b7;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.store8
    public static void x20203A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b0;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.store16
    public static void x20203B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b0;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].b].b1;
        ++state.ip;
        ++state.ip;
    }

    // local.local.132.eq
    public static void x202046(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 =
            (uint)(state.locals[state.program[state.ip].a].i32 ==
                   state.locals[state.program[state.ip].b].i32
                ? 1
                : 0);
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.132.ne
    public static void x202047(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 =
            (uint)(state.locals[state.program[state.ip].a].i32 !=
                   state.locals[state.program[state.ip].b].i32
                ? 1
                : 0);
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.lt_s
    public static void x202048(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 =
            (uint)((int)state.locals[state.program[state.ip].a].i32 <
                   (int)state.locals[state.program[state.ip].b].i32
                ? 1
                : 0);
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.lt_u
    public static void x202049(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 =
            state.locals[state.program[state.ip].a].i32 < state.locals[state.program[state.ip].b].i32
                ? 1
                : (uint)0;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.gt_s
    public static void x20204A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 =
            (uint)((int)state.locals[state.program[state.ip].a].i32 >
                   (int)state.locals[state.program[state.ip].b].i32
                ? 1
                : 0);
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.gt_u
    public static void x20204B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 =
            state.locals[state.program[state.ip].a].i32 > state.locals[state.program[state.ip].b].i32
                ? 1
                : (uint)0;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.132.add
    public static void x20206A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 = state.locals[state.program[state.ip].a].i32 +
                                       state.locals[state.program[state.ip].b].i32;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }
}
