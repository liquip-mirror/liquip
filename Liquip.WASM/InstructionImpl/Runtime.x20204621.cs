#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    /* 32-bit */

    // local.local.i32.eq.local
    public static void x20204621(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 =
            (uint)(state.locals[state.program[state.ip].a].i32 ==
                   state.locals[state.program[state.ip].b].i32
                ? 1
                : 0);
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.ne.local
    public static void x20204721(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 =
            (uint)(state.locals[state.program[state.ip].a].i32 !=
                   state.locals[state.program[state.ip].b].i32
                ? 1
                : 0);
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.lt_s.local
    public static void x20204821(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 =
            (uint)((int)state.locals[state.program[state.ip].a].i32 <
                   (int)state.locals[state.program[state.ip].b].i32
                ? 1
                : 0);
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.lt_u.local
    public static void x20204921(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 = state.locals[state.program[state.ip].a].i32 <
                                                         state.locals[state.program[state.ip].b].i32
            ? 1
            : (uint)0;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.gt_s.local
    public static void x20204A21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 =
            (uint)((int)state.locals[state.program[state.ip].a].i32 >
                   (int)state.locals[state.program[state.ip].b].i32
                ? 1
                : 0);
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.gt_u.local
    public static void x20204B21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 = state.locals[state.program[state.ip].a].i32 >
                                                         state.locals[state.program[state.ip].b].i32
            ? 1
            : (uint)0;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.add.local
    public static void x20206A21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 = state.locals[state.program[state.ip].a].i32 +
                                                         state.locals[state.program[state.ip].b].i32;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.sub.local
    public static void x20206B21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 = state.locals[state.program[state.ip].a].i32 -
                                                         state.locals[state.program[state.ip].b].i32;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.mul.local
    public static void x20206C21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 = state.locals[state.program[state.ip].a].i32 *
                                                         state.locals[state.program[state.ip].b].i32;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.div_s.local
    public static void x20206D21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 =
            (uint)((int)state.locals[state.program[state.ip].a].i32 /
                   (int)state.locals[state.program[state.ip].b].i32);
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.div_u.local
    public static void x20206E21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 = state.locals[state.program[state.ip].a].i32 /
                                                         state.locals[state.program[state.ip].b].i32;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.rem_s.local
    public static void x20206F21(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 =
            (uint)(state.locals[state.program[state.ip].a].i32 == 0x80000000 &&
                   state.locals[state.program[state.ip].b].i32 == 0xFFFFFFFF
                ? 0
                : (int)state.locals[state.program[state.ip].a].i32 %
                  (int)state.locals[state.program[state.ip].b].i32);
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.rem_u.local
    public static void x20207021(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 = state.locals[state.program[state.ip].a].i32 -
                                                         state.locals[state.program[state.ip].b].i32;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.and.local
    public static void x20207121(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 = state.locals[state.program[state.ip].a].i32 &
                                                         state.locals[state.program[state.ip].b].i32;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.or.local
    public static void x20207221(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 = state.locals[state.program[state.ip].a].i32 |
                                                         state.locals[state.program[state.ip].b].i32;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.xor.local
    public static void x20207321(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 = state.locals[state.program[state.ip].a].i32 ^
                                                         state.locals[state.program[state.ip].b].i32;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.shl.local
    public static void x20207421(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 =
            state.locals[state.program[state.ip].a].i32 <<
            (int)state.locals[state.program[state.ip].b].i32;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.shr_s.local
    public static void x20207521(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 =
            (uint)((int)state.locals[state.program[state.ip].a].i32 >>
                   (int)state.locals[state.program[state.ip].b].i32);
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.shr_u.local
    public static void x20207621(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 =
            state.locals[state.program[state.ip].a].i32 >>
            (int)state.locals[state.program[state.ip].b].i32;
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.rotl.local
    public static void x20207721(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 =
            (state.locals[state.program[state.ip].a].i32 <<
             (int)state.locals[state.program[state.ip].b].i32) |
            (state.locals[state.program[state.ip].a].i32 >>
             (32 - (int)state.locals[state.program[state.ip].b].i32));
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

    // local.local.i32.rotr.local
    public static void x20207821(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.locals[state.program[state.ip].c].type = Type.i32;
        state.locals[state.program[state.ip].c].i32 =
            (state.locals[state.program[state.ip].a].i32 >>
             (int)state.locals[state.program[state.ip].b].i32) |
            (state.locals[state.program[state.ip].a].i32 <<
             (32 - (int)state.locals[state.program[state.ip].b].i32));
        ++state.ip;
        ++state.ip;
        ++state.ip;
    }

}
