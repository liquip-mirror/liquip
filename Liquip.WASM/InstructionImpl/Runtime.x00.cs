#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// ReSharper disable InconsistentNaming
// ReSharper disable All


using Liquip.WASM.Instruction;

namespace Liquip.WASM.VM;

public partial class InstructionImpl
{

    // unreachable
    public static void x00(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        throw new TrapException("unreachable");
    }

    // nop
    public static void x01(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
    }

    // block
    public static void x02(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        // PushLabel
        state.lStack[state.labelPtr].ip = state.program[state.ip].pos;
        state.lStack[state.labelPtr].vStackPtr = state.vStackPtr;
        state.lStack[state.labelPtr].i = state.program[state.ip].i;
        ++state.labelPtr;
    }

    // loop
    public static void x03(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        // PushLabel
        state.lStack[state.labelPtr].ip = state.ip - 1;
        state.lStack[state.labelPtr].vStackPtr = state.vStackPtr;
        state.lStack[state.labelPtr].i = state.program[state.ip].i;
        ++state.labelPtr;
    }

    // if
    public static void x04(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        if (vStack[--state.vStackPtr].i32 > 0)
        {
            if (state.program[state.program[state.ip].pos].opCode == 0x05) // if it's an else
            {
                // PushLabel
                state.lStack[state.labelPtr].ip = state.program[state.program[state.ip].pos].pos;
                state.lStack[state.labelPtr].vStackPtr = state.vStackPtr;
                state.lStack[state.labelPtr].i = state.program[state.ip].i;
                ++state.labelPtr;
            }
            else
            {
                // PushLabel
                state.lStack[state.labelPtr].ip = state.program[state.ip].pos;
                state.lStack[state.labelPtr].vStackPtr = state.vStackPtr;
                state.lStack[state.labelPtr].i = state.program[state.ip].i;
                ++state.labelPtr;
            }
        }
        else
        {
            if (state.program[state.program[state.ip].pos].opCode == 0x05) // if it's an else
            {
                // PushLabel
                state.lStack[state.labelPtr].ip = state.program[state.program[state.ip].pos].pos;
                state.lStack[state.labelPtr].vStackPtr = state.vStackPtr;
                state.lStack[state.labelPtr].i = state.program[state.ip].i;
                ++state.labelPtr;
            }

            state.ip = state.program[state.ip].pos;
        }
    }

    // else
    public static void x05(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        state.ip = state.program[state.ip].pos;
    }

    // end
    public static void x0B(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        // If special case of end of a function, just get out of here.
        // if (state.ip + 1 == state.program.Length) break;
        if (state.labelPtr > 0)
        {
            label = state.lStack[state.labelPtr - 1];
            if (label.i.type != 0x40)
            {
                vStack[label.vStackPtr] = vStack[--state.vStackPtr];
                label.vStackPtr++;
            }

            state.labelPtr -= 1;
            state.vStackPtr = state.lStack[state.labelPtr].vStackPtr;
        }
    }

    // br
    public static void x0C(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
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

    // br_if
    public static void x0D(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        if (vStack[state.vStackPtr].i32 > 0)
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
    }

    // br_table
    public static void x0E(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
        index = (int)vStack[state.vStackPtr].i32;

        if ((uint)index >= state.program[state.ip].table.Length)
        {
            index = state.program[state.ip].pos;
        }
        else
        {
            index = state.program[state.ip].table[index];
        }

        if (state.labelPtr - index >= 0)
        {
            label = state.lStack[state.labelPtr - index];
            if (!(label.i is Loop) && label.i.type != 0x40)
            {
                vStack[label.vStackPtr] = vStack[--state.vStackPtr];
                label.vStackPtr++;
            }

            state.labelPtr -= index;
            state.vStackPtr = state.lStack[state.labelPtr].vStackPtr;

            state.ip = state.lStack[state.labelPtr].ip;
        }
        else
        {
            state.ip = state.program.Length - 2;
        }
    }

    // return
    public static void x0F(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        for (i = 0; i < state.function.Type.Results.Length; ++i)
        {
            vStack[cStack[cStackPtr - 1].vStackPtr++] = vStack[state.vStackPtr - 1 - i];
        }

        state = cStack[--cStackPtr];
        if (cStackPtr == 0)
        {
            // return false;
        }
    }

    // call
    public static void x10(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        index = state.function.BaseModule.functions[state.program[state.ip].pos]
            .GlobalIndex; // TODO: this may need to be optimized

        if (functions[index].program == null) // native
        {
            var parameters = new Value[functions[index].Type.Parameters.Length];
            for (i = functions[index].Type.Parameters.Length - 1; i >= 0; --i)
            {
                parameters[i] = vStack[--state.vStackPtr];
            }

            var returns = functions[index].native(parameters);

            for (i = 0; i < returns.Length; ++i)
            {
                vStack[state.vStackPtr++] = returns[i];
            }
        }
        else
        {
            state = cStack[++cStackPtr];
            state.ip = -1;
            state.function = functions[index];
            state.program = state.function.program;
            state.labelPtr = 0;
            if (state.function.BaseModule.Memory.Count > 0)
            {
                state.memory = state.function.BaseModule.Memory[0];
            }
            else
            {
                state.memory = null;
            }

            if (state.function.Type.Parameters.Length > 0)
            {
                cStack[cStackPtr - 1].vStackPtr -= state.function.Type.Parameters.Length;
                Array.Copy(vStack, cStack[cStackPtr - 1].vStackPtr, state.locals, 0,
                    state.function.Type.Parameters.Length);
            }

            state.vStackPtr = cStack[cStackPtr - 1].vStackPtr;

            if (state.function.LocalTypes.Length > 0)
            {
                Array.Copy(functions[index].LocalTypes, 0, state.locals,
                    state.function.Type.Parameters.Length,
                    functions[index].LocalTypes.Length);
            }
        }
    }

    // call_indirect
    public static void x11(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        index = state.function.BaseModule
            .functions[
                state.function.BaseModule.Tables[state.program[state.ip].pos]
                    .Get(vStack[--state.vStackPtr].i32)]
            .GlobalIndex;

        state = cStack[++cStackPtr];

        state.ip = -1;
        state.function = functions[index];
        state.program = state.function.program;
        state.labelPtr = 0;
        if (state.function.BaseModule.Memory.Count > 0)
        {
            state.memory = state.function.BaseModule.Memory[0];
        }
        else
        {
            state.memory = null;
        }


        if (state.function.Type.Parameters.Length > 0)
        {
            cStack[cStackPtr - 1].vStackPtr -= state.function.Type.Parameters.Length;
            Array.Copy(vStack, cStack[cStackPtr - 1].vStackPtr, state.locals, 0,
                state.function.Type.Parameters.Length);
        }

        state.vStackPtr = cStack[cStackPtr - 1].vStackPtr;

        if (state.function.LocalTypes.Length > 0)
        {
            Array.Copy(functions[index].LocalTypes, 0, state.locals,
                state.function.Type.Parameters.Length,
                functions[index].LocalTypes.Length);
        }
    }

    // drop
    public static void x1A(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i)
    {
        --state.vStackPtr;
    }

}
