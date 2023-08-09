#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{
    /* 64-bit */

    // local.local.i64.add.local
    public static void x20207C21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 = state.locals[state.program[state.ip].a].i64 +
                                                         state.locals[state.program[state.ip].b].i64;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.sub.local
    public static void x20207D21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 = state.locals[state.program[state.ip].a].i64 -
                                                         state.locals[state.program[state.ip].b].i64;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.mul.local
    public static void x20207E21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 = state.locals[state.program[state.ip].a].i64 *
                                                         state.locals[state.program[state.ip].b].i64;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.div_s.local
    public static void x20207F21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 =
            (ulong)((long)state.locals[state.program[state.ip].a].i64 /
                    (long)state.locals[state.program[state.ip].b].i64);
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.div_u.local
    public static void x20208021(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 = state.locals[state.program[state.ip].a].i64 /
                                                         state.locals[state.program[state.ip].b].i64;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.rem_s.local
    public static void x20208121(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 =
            (ulong)(state.locals[state.program[state.ip].a].i64 == 0x8000000000000000 &&
                    state.locals[state.program[state.ip].b].i64 == 0xFFFFFFFFFFFFFFFF
                ? 0
                : (long)state.locals[state.program[state.ip].a].i64 %
                  (long)state.locals[state.program[state.ip].b].i64);
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.rem_u.local
    public static void x20208221(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 = state.locals[state.program[state.ip].a].i64 %
                                                         state.locals[state.program[state.ip].b].i64;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }


    // local.local.i64.and.local
    public static void x20208321(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 = state.locals[state.program[state.ip].a].i64 &
                                                         state.locals[state.program[state.ip].b].i64;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }


    // local.local.i64.or.local
    public static void x20208421(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 = state.locals[state.program[state.ip].a].i64 |
                                                         state.locals[state.program[state.ip].b].i64;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.xor.local
    public static void x20208521(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 = state.locals[state.program[state.ip].a].i64 ^
                                                         state.locals[state.program[state.ip].b].i64;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.shl.local
    public static void x20208621(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 =
            state.locals[state.program[state.ip].a].i64 <<
            (int)state.locals[state.program[state.ip].b].i64;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.shr_s.local
    public static void x20208721(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 =
            (ulong)((long)state.locals[state.program[state.ip].a].i64 >>
                    (int)state.locals[state.program[state.ip].b].i64);
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.shr_u.local
    public static void x20208821(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 =
            state.locals[state.program[state.ip].a].i64 >> (int)state.locals[state.program[state.ip].b].i64;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.rotl.local
    public static void x20208921(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 =
            (state.locals[state.program[state.ip].a].i64 << (int)state.locals[state.program[state.ip].b].i64) |
            (state.locals[state.program[state.ip].a].i64 >> (64 - (int)state.locals[state.program[state.ip].b].i64));
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i64.rotr.local
    public static void x20208A21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i64;
        state.locals[state.program[state.ip].c].i64 =
            (state.locals[state.program[state.ip].a].i64 >> (int)state.locals[state.program[state.ip].b].i64) |
            (state.locals[state.program[state.ip].a].i64 << (64 - (int)state.locals[state.program[state.ip].b].i64));
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // loop of local.i32.load.local
    public static void xFF000000(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        length = state.ip;
        for (i = 0; i < state.program[length].optimalProgram.Length; ++i)
        {
            offset = state.program[length].optimalProgram[i].pos64 +
                     state.locals[state.program[length].optimalProgram[i].a].i32;
            index = state.program[length].optimalProgram[i].b;
            state.locals[index].b0 = state.memory.Buffer[offset];
            ++offset;
            state.locals[index].b1 = state.memory.Buffer[offset];
            ++offset;
            state.locals[index].b2 = state.memory.Buffer[offset];
            ++offset;
            state.locals[index].b3 = state.memory.Buffer[offset];
            ++state.ip;
            ++state.ip;
            ++state.ip;
        }

        --state.ip;
    }


    // loop of i32.const.local
    public static void xFE000000(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        index = state.ip;
        for (i = 0; i < state.program[index].optimalProgram.Length; ++i)
        {
            state.locals[state.program[index].optimalProgram[i].a].i32 = state.program[index].optimalProgram[i].i32;
            ++state.ip;
            ++state.ip;
        }

        --state.ip;
    }

}
