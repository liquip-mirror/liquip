#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // local.local.f64.div
    public static void x2020A3(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 = state.locals[state.program[state.ip].a].f64 /
                                       state.locals[state.program[state.ip].b].f64;
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.f64.min
    public static void x2020A4(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 =
            Math.Min(state.locals[state.program[state.ip].a].f64,
                state.locals[state.program[state.ip].b].f64);
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.local.f64.max
    public static void x2020A5(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].f64 =
            Math.Max(state.locals[state.program[state.ip].a].f64,
                state.locals[state.program[state.ip].b].f64);
        ++state.vStackPtr;
        ++state.ip;
        ++state.ip;
    }

    // local.i32.load.local
    public static void x202821(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        index = state.program[state.ip].b;
        state.locals[index].b0 = state.memory.Buffer[offset];
        ++offset;
        state.locals[index].b1 = state.memory.Buffer[offset];
        ++offset;
        state.locals[index].b2 = state.memory.Buffer[offset];
        ++offset;
        state.locals[index].b3 = state.memory.Buffer[offset];
        ++state.ip;
        ++state.ip;
    }

    // local.i64.load.local
    public static void x202921(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
        => x202B21(ref functions, ref cStackPtr, ref state, ref cStack, ref vStack, ref label, ref length, ref index, ref offset, ref i);

    // local.f64.load.local
    public static void x202B21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        state.locals[state.program[state.ip].b].b0 = state.memory.Buffer[offset];
        ++offset;
        state.locals[state.program[state.ip].b].b1 = state.memory.Buffer[offset];
        ++offset;
        state.locals[state.program[state.ip].b].b2 = state.memory.Buffer[offset];
        ++offset;
        state.locals[state.program[state.ip].b].b3 = state.memory.Buffer[offset];
        ++offset;
        state.locals[state.program[state.ip].b].b4 = state.memory.Buffer[offset];
        ++offset;
        state.locals[state.program[state.ip].b].b5 = state.memory.Buffer[offset];
        ++offset;
        state.locals[state.program[state.ip].b].b6 = state.memory.Buffer[offset];
        ++offset;
        state.locals[state.program[state.ip].b].b7 = state.memory.Buffer[offset];
        ++state.ip;
        ++state.ip;
    }

    // local.i32.load8_s.local
    public static void x202C21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        state.locals[state.program[state.ip].b].i32 = (uint)(sbyte)state.memory.Buffer[offset];
        ++state.ip;
        ++state.ip;
    }

    // local.i32.load8_u.local
    public static void x202D21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        state.locals[state.program[state.ip].b].i32 = state.memory.Buffer[offset];
        ++state.ip;
        ++state.ip;
    }

    // local.i32.load16_s.local
    public static void x202E21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        state.locals[state.program[state.ip].b].i32 =
            (uint)(short)(state.memory.Buffer[offset] | (ushort)(state.memory.Buffer[offset + 1] << 8));
        ++state.ip;
        ++state.ip;
    }

    // local.i32.load16_u.local
    public static void x202F21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        offset = state.program[state.ip].pos64 + state.locals[state.program[state.ip].a].i32;
        state.locals[state.program[state.ip].b].i32 =
            state.memory.Buffer[offset] | (uint)(state.memory.Buffer[offset + 1] << 8);
        ++state.ip;
        ++state.ip;
    }

    // local.f64.abs.local
    public static void x209921(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].b].f64 =
            Math.Abs(state.locals[state.program[state.ip].a].f64);
        ++state.ip;
        ++state.ip;
    }

    // local.f64.neg.local
    public static void x209A21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].b].f64 = -state.locals[state.program[state.ip].a].f64;
        ++state.ip;
        ++state.ip;
    }

    // local.f64.ceil.local
    public static void x209B21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].b].f64 =
            Math.Ceiling(state.locals[state.program[state.ip].a].f64);
        ++state.ip;
        ++state.ip;
    }

    // local.f64.floor.local
    public static void x209C21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].b].f64 =
            Math.Floor(state.locals[state.program[state.ip].a].f64);
        ++state.ip;
        ++state.ip;
    }
}
