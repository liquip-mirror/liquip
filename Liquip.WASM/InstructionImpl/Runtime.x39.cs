#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All


using Liquip.WASM.Instruction;

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // f64.store
    public static void x39(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.function.BaseModule.memory[0].SetI64(
            state.program[state.ip].pos64 + vStack[state.vStackPtr - 2].i32,
            vStack[state.vStackPtr - 1]
                .i64); // this may not work, but they point to the same location so ?
        state.vStackPtr -= 2;
    }

    // i32.store8
    public static void x3A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.memory.Buffer[state.program[state.ip].pos64 + vStack[state.vStackPtr - 2].i32] =
            vStack[state.vStackPtr - 1].b0;
        state.vStackPtr -= 2;
    }

    // i32.store16
    public static void x3B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.function.BaseModule.memory[0].SetI16(
            state.program[state.ip].pos64 + vStack[state.vStackPtr - 2].i32,
            (ushort)vStack[state.vStackPtr - 1].i32);
        state.vStackPtr -= 2;
    }

    // i64.store8
    public static void x3C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.memory.Buffer[state.program[state.ip].pos64 + vStack[state.vStackPtr - 2].i32] =
            vStack[state.vStackPtr - 1].b0;
        state.vStackPtr -= 2;
    }

    // i64.store16
    public static void x3D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.function.BaseModule.memory[0].SetI16(
            state.program[state.ip].pos64 + vStack[state.vStackPtr - 2].i32,
            (ushort)vStack[state.vStackPtr - 1].i64);
        state.vStackPtr -= 2;
    }

    // i64.store32
    public static void x3E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.function.BaseModule.memory[0].SetI32(
            state.program[state.ip].pos64 + vStack[state.vStackPtr - 2].i32,
            (uint)vStack[state.vStackPtr - 1].i64);
        state.vStackPtr -= 2;
    }

    // memory.size
    public static void x3F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 = (uint)state.function.BaseModule.memory[0].CurrentPages;
        ++state.vStackPtr;
    }

    // memory.grow
    public static void x40(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].type = Type.i32;
        vStack[state.vStackPtr].i32 =
            state.function.BaseModule.memory[0].Grow(vStack[state.vStackPtr].i32);
        ++state.vStackPtr;
    }

    // i32.const
    public static void x41(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr] = state.program[state.ip].value;
        ++state.vStackPtr;
    }

    // i64.const
    public static void x42(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr] = state.program[state.ip].value;
        ++state.vStackPtr;
    }

    // f32.const
    public static void x43(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr] = state.program[state.ip].value;
        ++state.vStackPtr;
    }

    // f64.const
    public static void x44(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr] = state.program[state.ip].value;
        ++state.vStackPtr;
    }

    // i32.eqz
    public static void x45(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].i32 =
            vStack[state.vStackPtr - 1].i32 == 0 ? 1 : (uint)0;
    }

    // i32.eq
    public static void x46(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 ==
            vStack[state.vStackPtr - 1].i32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i32.ne
    public static void x47(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 !=
            vStack[state.vStackPtr - 1].i32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i32.lt_s
    public static void x48(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            (int)vStack[state.vStackPtr - 2].i32 <
            (int)vStack[state.vStackPtr - 1].i32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i32.lt_u
    public static void x49(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 <
            vStack[state.vStackPtr - 1].i32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i32.gt_s
    public static void x4A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            (int)vStack[state.vStackPtr - 2].i32 >
            (int)vStack[state.vStackPtr - 1].i32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i32.gt_u
    public static void x4B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            vStack[state.vStackPtr - 2].i32 >
            vStack[state.vStackPtr - 1].i32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }

    // i32.le_s
    public static void x4C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 2].i32 =
            (int)vStack[state.vStackPtr - 2].i32 <=
            (int)vStack[state.vStackPtr - 1].i32
                ? 1
                : (uint)0;
        --state.vStackPtr;
    }
}
