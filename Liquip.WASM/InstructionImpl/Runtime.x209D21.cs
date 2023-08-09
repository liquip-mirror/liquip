#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // local.f64.trunc.local
    public static void x209D21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].b].f64 =
            Math.Truncate(state.locals[state.program[state.ip].a].f64);
        ++state.ip;
        ++state.ip;
    }

    // local.f64.nearest.local
    public static void x209E21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].b].f64 =
            Math.Round(state.locals[state.program[state.ip].a].f64);
        ++state.ip;
        ++state.ip;
    }

    // local.f64.sqrt.local
    public static void x209F21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].b].f64 =
            Math.Sqrt(state.locals[state.program[state.ip].a].f64);
        ++state.ip;
        ++state.ip;
    }

    // local.f64.add.local
    public static void x20A021(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        state.locals[state.program[state.ip].b].f64 = vStack[state.vStackPtr].f64 +
                                                         state.locals[state.program[state.ip].a].f64;
        ++state.ip;
        ++state.ip;
    }

    // local.f64.sub.local
    public static void x20A121(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        state.locals[state.program[state.ip].b].f64 = vStack[state.vStackPtr].f64 -
                                                         state.locals[state.program[state.ip].a].f64;
        ++state.ip;
        ++state.ip;
    }

    // local.f64.mul.local
    public static void x20A221(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        state.locals[state.program[state.ip].b].f64 = vStack[state.vStackPtr].f64 *
                                                         state.locals[state.program[state.ip].a].f64;
        ++state.ip;
        ++state.ip;
    }

    // local.f64.div.local
    public static void x20A321(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        state.locals[state.program[state.ip].b].f64 = vStack[state.vStackPtr].f64 /
                                                         state.locals[state.program[state.ip].a].f64;
        ++state.ip;
        ++state.ip;
    }

    // local.f64.min.local
    public static void x20A421(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        state.locals[state.program[state.ip].b].f64 =
            Math.Min(vStack[state.vStackPtr].f64, state.locals[state.program[state.ip].a].f64);
        ++state.ip;
        ++state.ip;
    }

    // local.f64.max.local
    public static void x20A521(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        state.locals[state.program[state.ip].b].f64 =
            Math.Max(vStack[state.vStackPtr].f64, state.locals[state.program[state.ip].a].f64);
        ++state.ip;
        ++state.ip;
    }

    // local.f64.copysign.local
    public static void x20A621(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        if (vStack[state.vStackPtr].f64 >= 0 && state.locals[state.program[state.ip].a].f64 < 0)
        {
            state.locals[state.program[state.ip].b].f64 =
                -state.locals[state.program[state.ip].a].f64;
        }

        if (vStack[state.vStackPtr].f64 < 0 && state.locals[state.program[state.ip].a].f64 >= 0)
        {
            state.locals[state.program[state.ip].b].f64 =
                -state.locals[state.program[state.ip].a].f64;
        }

        ++state.ip;
        ++state.ip;
    }

    // local.f64.convert_i32_s.local
    public static void x20B721(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].b].type = Type.f64;
        i = (int)state.locals[state.program[state.ip].a].i32;
        state.locals[state.program[state.ip].b].f64 = i;

        ++state.ip;
        ++state.ip;
    }

    // local.f64.convert_i32_u.local
    public static void x20B821(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].b].type = Type.f64;
        state.locals[state.program[state.ip].b].f64 = state.locals[state.program[state.ip].a].i32;
        ++state.ip;
        ++state.ip;
    }
}
