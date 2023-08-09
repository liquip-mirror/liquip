#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // local.copy
    public static void x2021(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].b] = state.locals[state.program[state.ip].a];
        ++state.ip;
    }

    // local.i32.load
    public static void x2028(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack,
        ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
        => x202A(ref functions, ref cStackPtr, ref state, ref cStack, ref vStack, ref label, ref length, ref index,
            ref offset, ref i);
    // local.f32.load
    public static void x202A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        vStack[state.vStackPtr].b0 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b1 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b2 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b3 = state.memory.Buffer[offset];
        ++state.ip;
        state.vStackPtr++;
    }

    // local.i64.load
    public static void x2029(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    => x202B(ref functions, ref cStackPtr, ref state, ref cStack, ref vStack, ref label, ref length, ref index, ref offset, ref i);

    // local.f64.load
    public static void x202B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        vStack[state.vStackPtr].b0 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b1 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b2 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b3 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b4 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b5 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b6 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b7 = state.memory.Buffer[offset];
        ++state.ip;
        state.vStackPtr++;
    }

    // local.i32.load8_s
    public static void x202C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        vStack[state.vStackPtr].i32 = (uint)(sbyte)state.memory.Buffer[offset];
        ++state.ip;
        state.vStackPtr++;
    }

    // local.i32.load8_u
    public static void x202D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        vStack[state.vStackPtr].i32 = state.memory.Buffer[offset];
        ++state.ip;
        state.vStackPtr++;
    }

    // local.i32.load16_s
    public static void x202E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        vStack[state.vStackPtr].i32 =
            (uint)(short)(state.memory.Buffer[offset] | (ushort)(state.memory.Buffer[offset + 1] << 8));
        ++state.ip;
        state.vStackPtr++;
    }

    // local.i32.load16_u
    public static void x202F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        vStack[state.vStackPtr].i32 =
            state.memory.Buffer[offset] | (uint)(state.memory.Buffer[offset + 1] << 8);
        ++state.ip;
        state.vStackPtr++;
    }

    // local.i32.store
    public static void x2036(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    => x2038(ref functions, ref cStackPtr, ref state, ref cStack, ref vStack, ref label, ref length, ref index, ref offset, ref i);

    // local.f32.store
    public static void x2038(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        offset = state.program[state.ip].pos64 + vStack[state.vStackPtr].i32;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b0;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b1;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b2;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b3;
        ++state.ip;
    }

    // local.i64.store
    public static void x2037(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
        => x2039(ref functions, ref cStackPtr, ref state, ref cStack, ref vStack, ref label, ref length, ref index, ref offset, ref i);

    // local.f64.store
    public static void x2039(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        offset = state.program[state.ip].pos64 + vStack[state.vStackPtr].i32;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b0;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b1;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b2;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b3;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b4;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b5;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b6;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b7;
        ++state.ip;
    }

    // local.i32.store8
    public static void x203A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        offset = state.program[state.ip].pos64 + vStack[state.vStackPtr].i32;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b0;
        ++state.ip;
    }

    // local.i32.store16
    public static void x203B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        offset = state.program[state.ip].pos64 + vStack[state.vStackPtr].i32;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b0;
        ++offset;
        state.memory.Buffer[offset] = state.locals[state.program[state.ip].a].b1;
        ++state.ip;
    }

    // local.i32.eqz
    public static void x2045(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].i32 = state.locals[state.program[state.ip].a].i32 == 0 ? 1 : (uint)0;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.eq
    public static void x2046(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            state.locals[state.program[state.ip].a].i32 == vStack[state.vStackPtr].i32 ? 1 : (uint)0;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.ne
    public static void x2047(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 =
            state.locals[state.program[state.ip].a].i32 != vStack[state.vStackPtr].i32 ? 1 : (uint)0;
        ++state.vStackPtr;
        ++state.ip;
    }

    // local.i32.lt_s
    public static void x2048(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i32 = (int)vStack[state.vStackPtr].i32 <
                                       (int)state.locals[state.program[state.ip].a].i32
            ? 1
            : (uint)0;
        ++state.vStackPtr;
        ++state.ip;
    }

}
