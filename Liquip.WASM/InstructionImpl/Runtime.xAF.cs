#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All

using Liquip.WASM.Instruction;

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // i64.trunc_f32_u
    public static void xAF(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i64;
        vStack[state.vStackPtr - 1].i64 =
            (ulong)Math.Truncate(vStack[state.vStackPtr - 1].f32);
    }

    // i64.trunc_f64_s
    public static void xB0(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i64;
        vStack[state.vStackPtr - 1].i64 =
            (ulong)(long)Math.Truncate(vStack[state.vStackPtr - 1].f64);
    }

    // i64.trunc_f64_u
    public static void xB1(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i64;
        vStack[state.vStackPtr - 1].i64 =
            (ulong)Math.Truncate(vStack[state.vStackPtr - 1].f64);
    }

    // f32.convert_i32_s
    public static void xB2(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f32;
        i = (int)vStack[state.vStackPtr - 1].i32;
        vStack[state.vStackPtr - 1].f32 = i;

    }

    // f32.convert_i32_u
    public static void xB3(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f32;
        vStack[state.vStackPtr - 1].f32 =
            vStack[state.vStackPtr - 1].i32;
    }

    // f32.convert_i64_s
    public static void xB4(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f32;
        var t = (long)vStack[state.vStackPtr - 1].i64;
        vStack[state.vStackPtr - 1].f32 = t;

    }

    // f32.convert_i64_u
    public static void xB5(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f32;
        vStack[state.vStackPtr - 1].f32 =
            vStack[state.vStackPtr - 1].i64;
    }

    // f32.demote_f64
    public static void xB6(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f32;
        vStack[state.vStackPtr - 1].f32 =
            (float)vStack[state.vStackPtr - 1].f64;
    }

    // f64.convert_i32_s
    public static void xB7(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f64;
        i = (int)vStack[state.vStackPtr - 1].i32;
        vStack[state.vStackPtr - 1].f64 = i;
    }

    // f64.convert_i32_u
    public static void xB8(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f64;
        vStack[state.vStackPtr - 1].f64 =
            vStack[state.vStackPtr - 1].i32;
    }

    // f64.convert_i64_s
    public static void xB9(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f64;
        var l = (long)vStack[state.vStackPtr - 1].i64;
        vStack[state.vStackPtr - 1].f64 = l;

    }

    // f64.convert_i64.u
    public static void xBA(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f64;
        vStack[state.vStackPtr - 1].f64 =
            vStack[state.vStackPtr - 1].i64;
    }

    // f64.promote_f32
    public static void xBB(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f64;
        vStack[state.vStackPtr - 1].f64 =
            vStack[state.vStackPtr - 1].f32;
    }

    // i32.reinterpret_f32
    public static void xBC(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i32;
    }

    // i64.reinterpret_f64
    public static void xBD(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.i64;
    }

    // f32.reinterpret_i32
    public static void xBE(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f32;
    }

    // f64.reinterpret_i64
    public static void xBF(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        vStack[state.vStackPtr - 1].type = Type.f64;
    }

    // local.br_if
    public static void x200D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        if (state.locals[state.program[state.ip].a].i32 > 0)
        {
            if (state.labelPtr - state.program[state.ip].pos >= 0)
            {
                label = state.lStack[state.labelPtr - state.program[state.ip].pos];
                if (!(label.i is Loop) && label.i.type != 0x40)
                {
                    vStack[label.vStackPtr] = vStack[--state.vStackPtr];
                    label.vStackPtr++;
                }

                state.labelPtr -= state.program[state.ip].pos;
                state.vStackPtr = state.lStack[state.labelPtr].vStackPtr;

                state.ip = state.lStack[state.labelPtr].ip;
            }
            else
            {
                state.ip = state.program.Length - 2;
            }
        }
        else
        {
            ++state.ip;
        }
    }

}
