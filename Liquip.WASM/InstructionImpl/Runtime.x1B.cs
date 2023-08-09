#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All

using Liquip.WASM.Instruction;

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // select
    public static void x1B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        if (vStack[state.vStackPtr].i32 == 0)
        {
            --state.vStackPtr;
            vStack[state.vStackPtr - 1] = vStack[state.vStackPtr];
        }
        else
        {
            --state.vStackPtr;
        }
    }

    // local.get
    public static void x20(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr] = state.locals[state.program[state.ip].a];
        ++state.vStackPtr;
    }

    // local.set
    public static void x21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        state.locals[state.program[state.ip].a] = vStack[state.vStackPtr];
    }

    // local.tee
    public static void x22(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        state.locals[state.program[state.ip].a] = vStack[state.vStackPtr];
        ++state.vStackPtr;
    }

    // global.get
    public static void x23(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr] =
            state.function.BaseModule.globals[state.program[state.ip].a].GetValue();
        ++state.vStackPtr;
    }

    // global.set
    public static void x24(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        state.function.BaseModule.globals[state.program[state.ip].a].Set(vStack[state.vStackPtr]);
    }

    // i32.load
    public static void x28(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        offset = state.program[state.ip].pos64 + vStack[state.vStackPtr].i32;
        vStack[state.vStackPtr].b0 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b1 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b2 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b3 = state.memory.Buffer[offset];
        ++state.vStackPtr;
    }

    // i64.load
    public static void x29(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        vStack[state.vStackPtr].i64 = BitConverter.ToUInt64(state.memory.Buffer,
            (int)state.program[state.ip].pos64 + (int)vStack[state.vStackPtr].i32);
        ++state.vStackPtr;
    }

    // f32.load
    public static void x2A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f32 = state.function
            .BaseModule.memory[0]
            .GetF32(state.program[state.ip].pos64 + vStack[state.vStackPtr - 1].i32);
    }

    // f64.load
    public static void x2B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].f64 = state.function
            .BaseModule.memory[0]
            .GetF64(state.program[state.ip].pos64 + vStack[state.vStackPtr - 1].i32);
    }

    // i32.load8_s
    public static void x2C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].i32 =
            (uint)(sbyte)state.memory.Buffer[
                state.program[state.ip].pos64 + vStack[state.vStackPtr - 1].i32];
    }

    // i32.load8_u
    public static void x2D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].i32 =
            state.memory.Buffer[state.program[state.ip].pos64 + vStack[state.vStackPtr - 1].i32];
    }

    // i32.load16_s
    public static void x2E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].i32 = state.function
            .BaseModule.memory[0]
            .GetI3216s(state.program[state.ip].pos64 + vStack[state.vStackPtr - 1].i32);
    }

    // i32.load16_u
    public static void x2F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        offset = state.program[state.ip].pos64 + vStack[state.vStackPtr].i32;
        vStack[state.vStackPtr].b0 = state.memory.Buffer[offset];
        ++offset;
        vStack[state.vStackPtr].b1 = state.memory.Buffer[offset];
        vStack[state.vStackPtr].b2 = 0;
        vStack[state.vStackPtr].b3 = 0;
        ++state.vStackPtr;
    }

    // i64.load8_s
    public static void x30(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].i64 = state.function
            .BaseModule.memory[0]
            .GetI648s(state.program[state.ip].pos64 + vStack[state.vStackPtr - 1].i32);
    }

    // i64.load8_u
    public static void x31(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].i64 = state.function
            .BaseModule.memory[0]
            .GetI648u(state.program[state.ip].pos64 + vStack[state.vStackPtr - 1].i32);
    }

    // i64.load16_s
    public static void x32(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].i64 = state.function
            .BaseModule.memory[0]
            .GetI6416s(state.program[state.ip].pos64 + vStack[state.vStackPtr - 1].i32);
    }

    // i64.load16_u
    public static void x33(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].i64 = state.function
            .BaseModule.memory[0]
            .GetI6416u(state.program[state.ip].pos64 + vStack[state.vStackPtr - 1].i32);
    }

    // i64.load32_s
    public static void x34(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].i64 = state.function
            .BaseModule.memory[0]
            .GetI6432s(state.program[state.ip].pos64 + vStack[state.vStackPtr - 1].i32);
    }

    // i64.load32_u
    public static void x35(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].i64 = state.function
            .BaseModule.memory[0]
            .GetI6432u(state.program[state.ip].pos64 + vStack[state.vStackPtr - 1].i32);
    }

    // i32.store
    public static void x36(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        --state.vStackPtr;
        offset = state.program[state.ip].pos64 + vStack[state.vStackPtr].i32;
        ++state.vStackPtr;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b0;
        ++offset;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b1;
        ++offset;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b2;
        ++offset;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b3;
        --state.vStackPtr;
    }

    // i64.store
    public static void x37(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        --state.vStackPtr;
        offset = state.program[state.ip].pos64 + vStack[state.vStackPtr].i32;
        ++state.vStackPtr;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b0;
        ++offset;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b1;
        ++offset;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b2;
        ++offset;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b3;
        ++offset;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b4;
        ++offset;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b5;
        ++offset;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b6;
        ++offset;
        state.memory.Buffer[offset] = vStack[state.vStackPtr].b7;
        --state.vStackPtr;
    }

    // f32.store
    public static void x38(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.function.BaseModule.memory[0].SetI32(
            state.program[state.ip].pos64 + vStack[state.vStackPtr - 2].i32,
            vStack[state.vStackPtr - 1]
                .i32); // this may not work, but they point to the same location so ?
        state.vStackPtr -= 2;
    }

}
