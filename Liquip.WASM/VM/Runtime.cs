using Liquip.WASM.Instruction;

namespace Liquip.WASM.VM;

public class Runtime
{

    /// <summary>
    ///
    /// </summary>
    public delegate void Op(ref Function[] functions, ref int cStackPtr, ref State state, ref State[] cStack, ref Value[] vStack, ref Label label, ref int length, ref int index, ref ulong offset, ref int i);

    private Function[] functions;
    private List<Function> _functions;
    private Value[] vStack; // Value stack
    private State[] cStack; // Call stack
    private int cStackPtr;
    private State _state; // the current state

    public int AddFunction(Function f)
    {
        _functions.Add(f);
        functions = _functions.ToArray();
        return functions.Length - 1;
    }

    public Runtime()
    {
        // TODO: these could be set differently
        cStack = new State[1000];
        for (var i = 0; i < 1000; i++)
        {
            cStack[i] = new State();
            cStack[i].locals = new Value[2000];
            cStack[i].lStack = new Label[1000];
            for (var o = 0; o < 1000; o++)
            {
                cStack[i].lStack[o] = new Label();
            }
        }

        vStack = new Value[1000];
        functions = new Function[0];
        _functions = new List<Function>();
    }

    // This should not be called internally, only for external use.
    public Value ReturnValue()
    {
        return vStack[0];
    }

    // Native call function
    public void Call(int functionIndex, object[]? parameters = null)
    {
        if (parameters == null)
        {
            parameters = new object[] { };
        }

        cStack[0].ip = 0;
        cStack[0].function = null;
        cStack[0].labelPtr = 0;
        cStack[0].vStackPtr = 0;
        cStackPtr = 1;
        cStack[1].ip = 0;
        cStack[1].function = functions[functionIndex];
        cStack[1].program = cStack[1].function.program;
        cStack[1].labelPtr = 0;
        cStack[1].vStackPtr = 0;
        _state = cStack[1];
        if (_state.function.BaseModule.Memory.Count > 0)
        {
            _state.memory = _state.function.BaseModule.Memory[0];
        }
        else
        {
            _state.memory = null;
        }

        var localPtr = 0;
        // TODO: check for matching type in FStat
        for (var i = 0; i < parameters.Length; i++)
        {
            if (parameters[i] is uint && _state.function.Type.Parameters[i] == Type.i32)
            {
                _state.locals[localPtr].type = Type.i32;
                _state.locals[localPtr].i32 = (uint)parameters[i];
            }
            else if (parameters[i] is ulong && _state.function.Type.Parameters[i] == Type.i64)
            {
                _state.locals[localPtr].type = Type.i64;
                _state.locals[localPtr].i64 = (ulong)parameters[i];
            }
            else if (parameters[i] is float && _state.function.Type.Parameters[i] == Type.f32)
            {
                _state.locals[localPtr].type = Type.f32;
                _state.locals[localPtr].f32 = (float)parameters[i];
            }
            else if (parameters[i] is double && _state.function.Type.Parameters[i] == Type.f64)
            {
                _state.locals[localPtr].type = Type.f64;
                _state.locals[localPtr].f64 = (double)parameters[i];
            }
            else
            {
                throw new TrapException("argument type mismatch");
            }

            localPtr++;
        }

        if (_state.function.LocalTypes.Length > 0)
        {
            Array.Copy(_state.function.LocalTypes, 0, _state.locals, localPtr, _state.function.LocalTypes.Length);
        }
    }

    // Returns true while there is still work to be done.
    public bool Step(int? steps)
    {
        // reusable variables for optimization:
        Label label = null;
        int length = 0;
        int index = 0;
        ulong offset = 0;
        int i = 0;

        do
        {

            InstructionImpl.Impl[_state.program[_state.ip].opCode].Invoke(
                ref functions,
                ref cStackPtr,
                ref _state,
                ref cStack,
                ref vStack,
                ref label,
                ref length,
                ref index,
                ref offset,
                ref i
                );


            ++_state.ip;
            --steps;
        } while (!steps.HasValue || steps > 0);

        if (cStackPtr == 0 && _state.ip >= _state.program.Length)
        {
            return false;
        }

        return true;
    }

}
