using Liquip.WASM.Instruction;

namespace Liquip.WASM.VM;

public class Runtime
{
    private Function[] functions;
    private readonly List<Function> _functions;
    private readonly Value[] vStack; // Value stack
    private readonly State[] cStack; // Call stack
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
        Label label;
        int length;
        int index;
        ulong offset;
        int i;

        do
        {

            switch (_state.program[_state.ip].opCode)
            {
                case 0x00: // unreachable
                    throw new TrapException("unreachable");
                case 0x01: // nop
                    break;
                case 0x02: // block
                    // PushLabel
                    _state.lStack[_state.labelPtr].ip = _state.program[_state.ip].pos;
                    _state.lStack[_state.labelPtr].vStackPtr = _state.vStackPtr;
                    _state.lStack[_state.labelPtr].i = _state.program[_state.ip].i;
                    ++_state.labelPtr;
                    break;
                case 0x03: // loop
                    // PushLabel
                    _state.lStack[_state.labelPtr].ip = _state.ip - 1;
                    _state.lStack[_state.labelPtr].vStackPtr = _state.vStackPtr;
                    _state.lStack[_state.labelPtr].i = _state.program[_state.ip].i;
                    ++_state.labelPtr;
                    break;
                case 0x04: // if
                    if (vStack[--_state.vStackPtr].i32 > 0)
                    {
                        if (_state.program[_state.program[_state.ip].pos].opCode == 0x05) // if it's an else
                        {
                            // PushLabel
                            _state.lStack[_state.labelPtr].ip = _state.program[_state.program[_state.ip].pos].pos;
                            _state.lStack[_state.labelPtr].vStackPtr = _state.vStackPtr;
                            _state.lStack[_state.labelPtr].i = _state.program[_state.ip].i;
                            ++_state.labelPtr;
                        }
                        else
                        {
                            // PushLabel
                            _state.lStack[_state.labelPtr].ip = _state.program[_state.ip].pos;
                            _state.lStack[_state.labelPtr].vStackPtr = _state.vStackPtr;
                            _state.lStack[_state.labelPtr].i = _state.program[_state.ip].i;
                            ++_state.labelPtr;
                        }
                    }
                    else
                    {
                        if (_state.program[_state.program[_state.ip].pos].opCode == 0x05) // if it's an else
                        {
                            // PushLabel
                            _state.lStack[_state.labelPtr].ip = _state.program[_state.program[_state.ip].pos].pos;
                            _state.lStack[_state.labelPtr].vStackPtr = _state.vStackPtr;
                            _state.lStack[_state.labelPtr].i = _state.program[_state.ip].i;
                            ++_state.labelPtr;
                        }

                        _state.ip = _state.program[_state.ip].pos;
                    }

                    break;
                case 0x05: // else
                    _state.ip = _state.program[_state.ip].pos;
                    break;
                case 0x0B: // end
                    // If special case of end of a function, just get out of here.
                    // if (_state.ip + 1 == _state.program.Length) break;
                    if (_state.labelPtr > 0)
                    {
                        label = _state.lStack[_state.labelPtr - 1];
                        if (label.i.type != 0x40)
                        {
                            vStack[label.vStackPtr] = vStack[--_state.vStackPtr];
                            label.vStackPtr++;
                        }

                        _state.labelPtr -= 1;
                        _state.vStackPtr = _state.lStack[_state.labelPtr].vStackPtr;
                    }

                    break;
                case 0x0C: // br
                    if (_state.labelPtr - _state.program[_state.ip].pos >= 0)
                    {
                        label = _state.lStack[_state.labelPtr - _state.program[_state.ip].pos];
                        if (!(label.i is Loop) && label.i.type != 0x40)
                        {
                            vStack[label.vStackPtr] = vStack[--_state.vStackPtr];
                            label.vStackPtr++;
                        }

                        _state.labelPtr -= _state.program[_state.ip].pos;
                        _state.vStackPtr = _state.lStack[_state.labelPtr].vStackPtr;

                        _state.ip = _state.lStack[_state.labelPtr].ip;
                    }
                    else
                    {
                        _state.ip = _state.program.Length - 2;
                    }

                    break;
                case 0x0D: // br_if
                    --_state.vStackPtr;
                    if (vStack[_state.vStackPtr].i32 > 0)
                    {
                        if (_state.labelPtr - _state.program[_state.ip].pos >= 0)
                        {
                            label = _state.lStack[_state.labelPtr - _state.program[_state.ip].pos];
                            if (!(label.i is Loop) && label.i.type != 0x40)
                            {
                                vStack[label.vStackPtr] = vStack[--_state.vStackPtr];
                                label.vStackPtr++;
                            }

                            _state.labelPtr -= _state.program[_state.ip].pos;
                            _state.vStackPtr = _state.lStack[_state.labelPtr].vStackPtr;

                            _state.ip = _state.lStack[_state.labelPtr].ip;
                        }
                        else
                        {
                            _state.ip = _state.program.Length - 2;
                        }
                    }

                    break;
                case 0x0E: // br_table
                    --_state.vStackPtr;
                    index = (int)vStack[_state.vStackPtr].i32;

                    if ((uint)index >= _state.program[_state.ip].table.Length)
                    {
                        index = _state.program[_state.ip].pos;
                    }
                    else
                    {
                        index = _state.program[_state.ip].table[index];
                    }

                    if (_state.labelPtr - index >= 0)
                    {
                        label = _state.lStack[_state.labelPtr - index];
                        if (!(label.i is Loop) && label.i.type != 0x40)
                        {
                            vStack[label.vStackPtr] = vStack[--_state.vStackPtr];
                            label.vStackPtr++;
                        }

                        _state.labelPtr -= index;
                        _state.vStackPtr = _state.lStack[_state.labelPtr].vStackPtr;

                        _state.ip = _state.lStack[_state.labelPtr].ip;
                    }
                    else
                    {
                        _state.ip = _state.program.Length - 2;
                    }

                    break;
                case 0x0F: // return

                    for (i = 0; i < _state.function.Type.Results.Length; ++i)
                    {
                        vStack[cStack[cStackPtr - 1].vStackPtr++] = vStack[_state.vStackPtr - 1 - i];
                    }

                    _state = cStack[--cStackPtr];
                    if (cStackPtr == 0)
                    {
                        return false;
                    }

                    break;
                case 0x10: // call
                    index = _state.function.BaseModule.functions[_state.program[_state.ip].pos]
                        .GlobalIndex; // TODO: this may need to be optimized

                    if (functions[index].program == null) // native
                    {
                        var parameters = new Value[functions[index].Type.Parameters.Length];
                        for (i = functions[index].Type.Parameters.Length - 1; i >= 0; --i)
                        {
                            parameters[i] = vStack[--_state.vStackPtr];
                        }

                        var returns = functions[index].native(parameters);

                        for (i = 0; i < returns.Length; ++i)
                        {
                            vStack[_state.vStackPtr++] = returns[i];
                        }
                    }
                    else
                    {
                        _state = cStack[++cStackPtr];
                        _state.ip = -1;
                        _state.function = functions[index];
                        _state.program = _state.function.program;
                        _state.labelPtr = 0;
                        if (_state.function.BaseModule.Memory.Count > 0)
                        {
                            _state.memory = _state.function.BaseModule.Memory[0];
                        }
                        else
                        {
                            _state.memory = null;
                        }

                        if (_state.function.Type.Parameters.Length > 0)
                        {
                            cStack[cStackPtr - 1].vStackPtr -= _state.function.Type.Parameters.Length;
                            Array.Copy(vStack, cStack[cStackPtr - 1].vStackPtr, _state.locals, 0,
                                _state.function.Type.Parameters.Length);
                        }

                        _state.vStackPtr = cStack[cStackPtr - 1].vStackPtr;

                        if (_state.function.LocalTypes.Length > 0)
                        {
                            Array.Copy(functions[index].LocalTypes, 0, _state.locals, _state.function.Type.Parameters.Length,
                                functions[index].LocalTypes.Length);
                        }
                    }

                    break;
                case 0x11: // call_indirect
                    index = _state.function.BaseModule
                        .functions[_state.function.BaseModule.Tables[_state.program[_state.ip].pos].Get(vStack[--_state.vStackPtr].i32)]
                        .GlobalIndex;

                    _state = cStack[++cStackPtr];

                    _state.ip = -1;
                    _state.function = functions[index];
                    _state.program = _state.function.program;
                    _state.labelPtr = 0;
                    if (_state.function.BaseModule.Memory.Count > 0)
                    {
                        _state.memory = _state.function.BaseModule.Memory[0];
                    }
                    else
                    {
                        _state.memory = null;
                    }


                    if (_state.function.Type.Parameters.Length > 0)
                    {
                        cStack[cStackPtr - 1].vStackPtr -= _state.function.Type.Parameters.Length;
                        Array.Copy(vStack, cStack[cStackPtr - 1].vStackPtr, _state.locals, 0,
                            _state.function.Type.Parameters.Length);
                    }

                    _state.vStackPtr = cStack[cStackPtr - 1].vStackPtr;

                    if (_state.function.LocalTypes.Length > 0)
                    {
                        Array.Copy(functions[index].LocalTypes, 0, _state.locals, _state.function.Type.Parameters.Length,
                            functions[index].LocalTypes.Length);
                    }

                    break;

                /* Parametric Instructions */

                case 0x1A: // drop
                    --_state.vStackPtr;
                    break;
                case 0x1B: // select
                    --_state.vStackPtr;
                    if (vStack[_state.vStackPtr].i32 == 0)
                    {
                        --_state.vStackPtr;
                        vStack[_state.vStackPtr - 1] = vStack[_state.vStackPtr];
                    }
                    else
                    {
                        --_state.vStackPtr;
                    }

                    break;

                /* Variable Instructions */

                case 0x20: // local.get
                    vStack[_state.vStackPtr] = _state.locals[_state.program[_state.ip].a];
                    ++_state.vStackPtr;
                    break;
                case 0x21: // local.set
                    --_state.vStackPtr;
                    _state.locals[_state.program[_state.ip].a] = vStack[_state.vStackPtr];
                    break;
                case 0x22: // local.tee
                    --_state.vStackPtr;
                    _state.locals[_state.program[_state.ip].a] = vStack[_state.vStackPtr];
                    ++_state.vStackPtr;
                    break;
                case 0x23: // global.get
                    vStack[_state.vStackPtr] = _state.function.BaseModule.globals[_state.program[_state.ip].a].GetValue();
                    ++_state.vStackPtr;
                    break;
                case 0x24: // global.set
                    --_state.vStackPtr;
                    _state.function.BaseModule.globals[_state.program[_state.ip].a].Set(vStack[_state.vStackPtr]);
                    break;

                /* Memory Instructions */

                case 0x28: // i32.load
                    --_state.vStackPtr;
                    offset = _state.program[_state.ip].pos64 + vStack[_state.vStackPtr].i32;
                    vStack[_state.vStackPtr].b0 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b1 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b2 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b3 = _state.memory.Buffer[offset];
                    ++_state.vStackPtr;
                    break;
                case 0x29: // i64.load
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i64 = BitConverter.ToUInt64(_state.memory.Buffer,
                        (int)_state.program[_state.ip].pos64 + (int)vStack[_state.vStackPtr].i32);
                    ++_state.vStackPtr;
                    break;
                case 0x2A: // f32.load
                    vStack[_state.vStackPtr - 1].f32 = _state.function
                        .BaseModule.memory[0]
                        .GetF32(_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 1].i32);
                    break;
                case 0x2B: // f64.load
                    vStack[_state.vStackPtr - 1].f64 = _state.function
                        .BaseModule.memory[0]
                        .GetF64(_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 1].i32);
                    break;
                case 0x2C: // i32.load8_s
                    vStack[_state.vStackPtr - 1].i32 =
                        (uint)(sbyte)_state.memory.Buffer[_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 1].i32];
                    break;
                case 0x2D: // i32.load8_u
                    vStack[_state.vStackPtr - 1].i32 = _state.memory.Buffer[_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 1].i32];
                    break;
                case 0x2E: // i32.load16_s
                    vStack[_state.vStackPtr - 1].i32 = _state.function
                        .BaseModule.memory[0]
                        .GetI3216s(_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 1].i32);
                    break;
                case 0x2F: // i32.load16_u
                    --_state.vStackPtr;
                    offset = _state.program[_state.ip].pos64 + vStack[_state.vStackPtr].i32;
                    vStack[_state.vStackPtr].b0 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b1 = _state.memory.Buffer[offset];
                    vStack[_state.vStackPtr].b2 = 0;
                    vStack[_state.vStackPtr].b3 = 0;
                    ++_state.vStackPtr;
                    break;
                case 0x30: // i64.load8_s
                    vStack[_state.vStackPtr - 1].i64 = _state.function
                        .BaseModule.memory[0]
                        .GetI648s(_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 1].i32);
                    break;
                case 0x31: // i64.load8_u
                    vStack[_state.vStackPtr - 1].i64 = _state.function
                        .BaseModule.memory[0]
                        .GetI648u(_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 1].i32);
                    break;
                case 0x32: // i64.load16_s
                    vStack[_state.vStackPtr - 1].i64 = _state.function
                        .BaseModule.memory[0]
                        .GetI6416s(_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 1].i32);
                    break;
                case 0x33: // i64.load16_u
                    vStack[_state.vStackPtr - 1].i64 = _state.function
                        .BaseModule.memory[0]
                        .GetI6416u(_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 1].i32);
                    break;
                case 0x34: // i64.load32_s
                    vStack[_state.vStackPtr - 1].i64 = _state.function
                        .BaseModule.memory[0]
                        .GetI6432s(_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 1].i32);
                    break;
                case 0x35: // i64.load32_u
                    vStack[_state.vStackPtr - 1].i64 = _state.function
                        .BaseModule.memory[0]
                        .GetI6432u(_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 1].i32);
                    break;

                /* TODO:THESE NEED TO BE OPTIMIZED TO NOT USE FUNCTION CALLS */
                case 0x36: // i32.store
                    --_state.vStackPtr;
                    --_state.vStackPtr;
                    offset = _state.program[_state.ip].pos64 + vStack[_state.vStackPtr].i32;
                    ++_state.vStackPtr;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b0;
                    ++offset;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b1;
                    ++offset;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b2;
                    ++offset;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b3;
                    --_state.vStackPtr;
                    break;

                case 0x37: // i64.store
                    --_state.vStackPtr;
                    --_state.vStackPtr;
                    offset = _state.program[_state.ip].pos64 + vStack[_state.vStackPtr].i32;
                    ++_state.vStackPtr;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b0;
                    ++offset;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b1;
                    ++offset;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b2;
                    ++offset;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b3;
                    ++offset;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b4;
                    ++offset;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b5;
                    ++offset;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b6;
                    ++offset;
                    _state.memory.Buffer[offset] = vStack[_state.vStackPtr].b7;
                    --_state.vStackPtr;
                    break;
                case 0x38: // f32.store
                    _state.function.BaseModule.memory[0].SetI32(
                        _state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 2].i32,
                        vStack[_state.vStackPtr - 1]
                            .i32); // this may not work, but they point to the same location so ?
                    _state.vStackPtr -= 2;
                    break;
                case 0x39: // f64.store
                    _state.function.BaseModule.memory[0].SetI64(
                        _state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 2].i32,
                        vStack[_state.vStackPtr - 1]
                            .i64); // this may not work, but they point to the same location so ?
                    _state.vStackPtr -= 2;
                    break;
                case 0x3A: // i32.store8
                    _state.memory.Buffer[_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 2].i32] = vStack[_state.vStackPtr - 1].b0;
                    _state.vStackPtr -= 2;
                    break;
                case 0x3B: // i32.store16
                    _state.function.BaseModule.memory[0].SetI16(
                        _state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 2].i32,
                        (ushort)vStack[_state.vStackPtr - 1].i32);
                    _state.vStackPtr -= 2;
                    break;
                case 0x3C: // i64.store8
                    _state.memory.Buffer[_state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 2].i32] = vStack[_state.vStackPtr - 1].b0;
                    _state.vStackPtr -= 2;
                    break;
                case 0x3D: // i64.store16
                    _state.function.BaseModule.memory[0].SetI16(
                        _state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 2].i32,
                        (ushort)vStack[_state.vStackPtr - 1].i64);
                    _state.vStackPtr -= 2;
                    break;
                case 0x3E: // i64.store32
                    _state.function.BaseModule.memory[0].SetI32(
                        _state.program[_state.ip].pos64 + vStack[_state.vStackPtr - 2].i32,
                        (uint)vStack[_state.vStackPtr - 1].i64);
                    _state.vStackPtr -= 2;
                    break;
                case 0x3F: // memory.size
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 = (uint)_state.function.BaseModule.memory[0].CurrentPages;
                    ++_state.vStackPtr;
                    break;
                case 0x40: // memory.grow
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 = _state.function.BaseModule.memory[0].Grow(vStack[_state.vStackPtr].i32);
                    ++_state.vStackPtr;
                    break;

                /* Numeric Instructions */

                // These could be optimized by passing the const values as already created Value types
                case 0x41: // i32.const
                    vStack[_state.vStackPtr] = _state.program[_state.ip].value;
                    ++_state.vStackPtr;
                    break;
                case 0x42: // i64.const
                    vStack[_state.vStackPtr] = _state.program[_state.ip].value;
                    ++_state.vStackPtr;
                    break;
                case 0x43: // f32.const
                {
                    vStack[_state.vStackPtr] = _state.program[_state.ip].value;
                    ++_state.vStackPtr;
                    break;
                }
                case 0x44: // f64.const
                {
                    vStack[_state.vStackPtr] = _state.program[_state.ip].value;
                    ++_state.vStackPtr;
                    break;
                }
                case 0x45: // i32.eqz
                    vStack[_state.vStackPtr - 1].i32 =
                        vStack[_state.vStackPtr - 1].i32 == 0 ? 1 : (uint)0;
                    break;
                case 0x46: // i32.eq
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 ==
                        vStack[_state.vStackPtr - 1].i32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x47: // i32.ne
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 !=
                        vStack[_state.vStackPtr - 1].i32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x48: // i32.lt_s
                    vStack[_state.vStackPtr - 2].i32 =
                        (int)vStack[_state.vStackPtr - 2].i32 <
                        (int)vStack[_state.vStackPtr - 1].i32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x49: // i32.lt_u
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 <
                        vStack[_state.vStackPtr - 1].i32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x4A: // i32.gt_s
                    vStack[_state.vStackPtr - 2].i32 =
                        (int)vStack[_state.vStackPtr - 2].i32 >
                        (int)vStack[_state.vStackPtr - 1].i32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x4B: // i32.gt_u
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 >
                        vStack[_state.vStackPtr - 1].i32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x4C: // i32.le_s
                    vStack[_state.vStackPtr - 2].i32 =
                        (int)vStack[_state.vStackPtr - 2].i32 <=
                        (int)vStack[_state.vStackPtr - 1].i32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x4D: // i32.le_u
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 <=
                        vStack[_state.vStackPtr - 1].i32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x4E: // i32.ge_s
                    vStack[_state.vStackPtr - 2].i32 =
                        (int)vStack[_state.vStackPtr - 2].i32 >=
                        (int)vStack[_state.vStackPtr - 1].i32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x4F: // i32.ge_u
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 >=
                        vStack[_state.vStackPtr - 1].i32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;

                case 0x50: // i64.eqz
                    vStack[_state.vStackPtr - 1].type = Type.i32;
                    vStack[_state.vStackPtr - 1].i32 =
                        vStack[_state.vStackPtr - 1].i64 == 0 ? 1 : (uint)0;
                    break;
                case 0x51: // i64.eq
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i64 ==
                        vStack[_state.vStackPtr - 1].i64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x52: // i64.ne
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i64 !=
                        vStack[_state.vStackPtr - 1].i64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x53: // i64.lt_s
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        (long)vStack[_state.vStackPtr - 2].i64 <
                        (long)vStack[_state.vStackPtr - 1].i64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x54: // i64.lt_u
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i64 <
                        vStack[_state.vStackPtr - 1].i64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x55: // i64.gt_s
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        (long)vStack[_state.vStackPtr - 2].i64 >
                        (long)vStack[_state.vStackPtr - 1].i64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x56: // i64.gt_u
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i64 >
                        vStack[_state.vStackPtr - 1].i64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x57: // i64.le_s
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        (long)vStack[_state.vStackPtr - 2].i64 <=
                        (long)vStack[_state.vStackPtr - 1].i64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x58: // i64.le_u
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i64 <=
                        vStack[_state.vStackPtr - 1].i64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x59: // i64.ge_s
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        (long)vStack[_state.vStackPtr - 2].i64 >=
                        (long)vStack[_state.vStackPtr - 1].i64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x5A: // i64.ge_u
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i64 >=
                        vStack[_state.vStackPtr - 1].i64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;

                case 0x5B: // f32.eq
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].f32.Equals(
                        vStack[_state.vStackPtr - 1].f32)
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x5C: // f32.ne
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        !vStack[_state.vStackPtr - 2].f32.Equals(
                        vStack[_state.vStackPtr - 1].f32)
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x5D: // f32.lt
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].f32 <
                        vStack[_state.vStackPtr - 1].f32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x5E: // f32.gt
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].f32 >
                        vStack[_state.vStackPtr - 1].f32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x5F: // f32.le
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].f32 <=
                        vStack[_state.vStackPtr - 1].f32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x60: // f32.ge
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].f32 >=
                        vStack[_state.vStackPtr - 1].f32
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;

                case 0x61: // f64.eq
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].f64 ==
                        vStack[_state.vStackPtr - 1].f64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x62: // f64.ne
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].f64 !=
                        vStack[_state.vStackPtr - 1].f64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x63: // f64.lt
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].f64 <
                        vStack[_state.vStackPtr - 1].f64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x64: // f64.gt
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].f64 >
                        vStack[_state.vStackPtr - 1].f64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x65: // f64.le
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].f64 <=
                        vStack[_state.vStackPtr - 1].f64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;
                case 0x66: // f64.ge
                    vStack[_state.vStackPtr - 2].type = Type.i32;
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].f64 >=
                        vStack[_state.vStackPtr - 1].f64
                            ? 1
                            : (uint)0;
                    --_state.vStackPtr;
                    break;

                case 0x67: // i32.clz
                {
                    // TODO: optimize this
                    var a = vStack[_state.vStackPtr - 1].i32;

                    uint bits = 0;
                    var compare = 0x80000000;
                    while (bits < 32)
                    {
                        if ((compare & a) == 0)
                        {
                            bits++;
                            compare >>= 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    vStack[_state.vStackPtr - 1].i32 = bits;
                    break;
                }
                case 0x68: // i32.ctz
                {
                    // TODO: optimize this
                    var a = vStack[_state.vStackPtr - 1].i32;

                    uint bits = 0;
                    uint compare = 1;
                    while (bits < 32)
                    {
                        if ((compare & a) == 0)
                        {
                            bits++;
                            compare <<= 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    vStack[_state.vStackPtr - 1].i32 = bits;

                    break;
                }
                case 0x69: // i32.popcnt
                {
                    // TODO: optimize this
                    var a = vStack[_state.vStackPtr - 1].i32;

                    uint bits = 0;
                    uint compare = 1;
                    while (true)
                    {
                        if ((compare & a) != 0)
                        {
                            bits++;
                        }

                        if (compare == 0x80000000)
                        {
                            break;
                        }

                        compare <<= 1;
                    }

                    vStack[_state.vStackPtr - 1].i32 = bits;

                    break;
                }
                case 0x6A: // i32.add
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 +
                        vStack[_state.vStackPtr - 1].i32;
                    --_state.vStackPtr;
                    break;
                case 0x6B: // i32.sub
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 -
                        vStack[_state.vStackPtr - 1].i32;
                    --_state.vStackPtr;
                    break;
                case 0x6C: // i32.mul
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 *
                        vStack[_state.vStackPtr - 1].i32;
                    --_state.vStackPtr;
                    break;
                case 0x6D: // i32.div_s
                    vStack[_state.vStackPtr - 2].i32 =
                        (uint)((int)vStack[_state.vStackPtr - 2].i32 /
                               (int)vStack[_state.vStackPtr - 1].i32);
                    --_state.vStackPtr;
                    break;
                case 0x6E: // i32.div_u
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 /
                        vStack[_state.vStackPtr - 1].i32;
                    --_state.vStackPtr;
                    break;
                case 0x6F: // i32.rem_s
                    vStack[_state.vStackPtr - 2].i32 =
                        (uint)(vStack[_state.vStackPtr - 2].i32 == 0x80000000 &&
                               vStack[_state.vStackPtr - 1].i32 == 0xFFFFFFFF
                            ? 0
                            : (int)vStack[_state.vStackPtr - 2].i32 %
                              (int)vStack[_state.vStackPtr - 1].i32);
                    --_state.vStackPtr;
                    break;
                case 0x70: // i32.rem_u
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 %
                        vStack[_state.vStackPtr - 1].i32;
                    --_state.vStackPtr;
                    break;
                case 0x71: // i32.and
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 &
                        vStack[_state.vStackPtr - 1].i32;
                    --_state.vStackPtr;
                    break;
                case 0x72: // i32.or
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 |
                        vStack[_state.vStackPtr - 1].i32;
                    --_state.vStackPtr;
                    break;
                case 0x73: // i32.xor
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 ^
                        vStack[_state.vStackPtr - 1].i32;
                    --_state.vStackPtr;
                    break;
                case 0x74: // i32.shl
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 <<
                        (int)vStack[_state.vStackPtr - 1].i32;
                    --_state.vStackPtr;
                    break;
                case 0x75: // i32.shr_s
                    vStack[_state.vStackPtr - 2].i32 =
                        (uint)((int)vStack[_state.vStackPtr - 2].i32 >>
                               (int)vStack[_state.vStackPtr - 1].i32);
                    --_state.vStackPtr;
                    break;
                case 0x76: // i32.shr_u
                    vStack[_state.vStackPtr - 2].i32 =
                        vStack[_state.vStackPtr - 2].i32 >>
                        (int)vStack[_state.vStackPtr - 1].i32;
                    --_state.vStackPtr;
                    break;
                case 0x77: // i32.rotl
                    vStack[_state.vStackPtr - 2].i32 = (vStack[_state.vStackPtr - 2].i32 << (int)vStack[_state.vStackPtr - 1].i32) |
                                                  (vStack[_state.vStackPtr - 2].i32 >>
                                                   (32 - (int)vStack[_state.vStackPtr - 1].i32));
                    --_state.vStackPtr;
                    break;
                case 0x78: // i32.rotr
                    vStack[_state.vStackPtr - 2].i32 = (vStack[_state.vStackPtr - 2].i32 >> (int)vStack[_state.vStackPtr - 1].i32) |
                                                  (vStack[_state.vStackPtr - 2].i32 <<
                                                   (32 - (int)vStack[_state.vStackPtr - 1].i32));
                    --_state.vStackPtr;
                    break;
                case 0x79: // i64.clz
                {
                    // TODO: optimize this
                    var a = vStack[_state.vStackPtr - 1].i64;

                    uint bits = 0;
                    var compare = 0x8000000000000000;
                    while (bits < 64)
                    {
                        if ((compare & a) == 0)
                        {
                            bits++;
                            compare >>= 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    vStack[_state.vStackPtr - 1].i64 = bits;
                    break;
                }
                case 0x7A: // i64.ctz
                {
                    // TODO: optimize this
                    var a = vStack[_state.vStackPtr - 1].i64;

                    ulong bits = 0;
                    ulong compare = 1;
                    while (bits < 64)
                    {
                        if ((compare & a) == 0)
                        {
                            bits++;
                            compare <<= 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    vStack[_state.vStackPtr - 1].i64 = bits;
                    break;
                }
                case 0x7B: // i64.popcnt
                {
                    // TODO: optimize this
                    var a = vStack[_state.vStackPtr - 1].i64;

                    ulong bits = 0;
                    ulong compare = 1;
                    while (true)
                    {
                        if ((compare & a) != 0)
                        {
                            bits++;
                        }

                        if (compare == 0x8000000000000000)
                        {
                            break;
                        }

                        compare <<= 1;
                    }

                    vStack[_state.vStackPtr - 1].i64 = bits;
                    break;
                }
                case 0x7C: // i64.add
                    vStack[_state.vStackPtr - 2].i64 =
                        vStack[_state.vStackPtr - 2].i64 +
                        vStack[_state.vStackPtr - 1].i64;
                    --_state.vStackPtr;
                    break;
                case 0x7D: // i64.sub
                    vStack[_state.vStackPtr - 2].i64 =
                        vStack[_state.vStackPtr - 2].i64 -
                        vStack[_state.vStackPtr - 1].i64;
                    --_state.vStackPtr;
                    break;
                case 0x7E: // i64.mul
                    vStack[_state.vStackPtr - 2].i64 =
                        vStack[_state.vStackPtr - 2].i64 *
                        vStack[_state.vStackPtr - 1].i64;
                    --_state.vStackPtr;
                    break;
                case 0x7F: // i64.div_s
                    vStack[_state.vStackPtr - 2].i64 =
                        (ulong)((long)vStack[_state.vStackPtr - 2].i64 /
                                (long)vStack[_state.vStackPtr - 1].i64);
                    --_state.vStackPtr;
                    break;
                case 0x80: // i64.div_u
                    vStack[_state.vStackPtr - 2].i64 =
                        vStack[_state.vStackPtr - 2].i64 /
                        vStack[_state.vStackPtr - 1].i64;
                    --_state.vStackPtr;
                    break;
                case 0x81: // i64.rem_s
                    vStack[_state.vStackPtr - 2].i64 =
                        (ulong)(vStack[_state.vStackPtr - 2].i64 == 0x8000000000000000 &&
                                vStack[_state.vStackPtr - 1].i64 == 0xFFFFFFFFFFFFFFFF
                            ? 0
                            : (long)vStack[_state.vStackPtr - 2].i64 %
                              (long)vStack[_state.vStackPtr - 1].i64);
                    --_state.vStackPtr;
                    break;
                case 0x82: // i64.rem_u
                    vStack[_state.vStackPtr - 2].i64 =
                        vStack[_state.vStackPtr - 2].i64 %
                        vStack[_state.vStackPtr - 1].i64;
                    --_state.vStackPtr;
                    break;
                case 0x83: // i64.and
                    vStack[_state.vStackPtr - 2].i64 =
                        vStack[_state.vStackPtr - 2].i64 &
                        vStack[_state.vStackPtr - 1].i64;
                    --_state.vStackPtr;
                    break;
                case 0x84: // i64.or
                    vStack[_state.vStackPtr - 2].i64 =
                        vStack[_state.vStackPtr - 2].i64 |
                        vStack[_state.vStackPtr - 1].i64;
                    --_state.vStackPtr;
                    break;
                case 0x85: // i64.xor
                    vStack[_state.vStackPtr - 2].i64 =
                        vStack[_state.vStackPtr - 2].i64 ^
                        vStack[_state.vStackPtr - 1].i64;
                    --_state.vStackPtr;
                    break;
                case 0x86: // i64.shl
                    vStack[_state.vStackPtr - 2].i64 =
                        vStack[_state.vStackPtr - 2].i64 <<
                        (int)vStack[_state.vStackPtr - 1].i64;
                    --_state.vStackPtr;
                    break;
                case 0x87: // i64.shr_s
                    vStack[_state.vStackPtr - 2].i64 =
                        (ulong)((long)vStack[_state.vStackPtr - 2].i64 >>
                                (int)vStack[_state.vStackPtr - 1].i64);
                    --_state.vStackPtr;
                    break;
                case 0x88: // i64.shr_u
                    vStack[_state.vStackPtr - 2].i64 =
                        vStack[_state.vStackPtr - 2].i64 >>
                        (int)vStack[_state.vStackPtr - 1].i64;
                    --_state.vStackPtr;
                    break;
                case 0x89: // i64.rotl
                    vStack[_state.vStackPtr - 2].i64 = (vStack[_state.vStackPtr - 2].i64 << (int)vStack[_state.vStackPtr - 1].i64) |
                                                  (vStack[_state.vStackPtr - 2].i64 >>
                                                   (64 - (int)vStack[_state.vStackPtr - 1].i64));
                    --_state.vStackPtr;
                    break;
                case 0x8A: // i64.rotr
                    vStack[_state.vStackPtr - 2].i64 =
                        (vStack[_state.vStackPtr - 2].i64 >>
                         (int)vStack[_state.vStackPtr - 1].i64) |
                        (vStack[_state.vStackPtr - 2].i64 <<
                         (64 - (int)vStack[_state.vStackPtr - 1].i64));
                    --_state.vStackPtr;
                    break;

                case 0x8B: // f32.abs
                    vStack[_state.vStackPtr - 1].f32 =
                        Math.Abs(vStack[_state.vStackPtr - 1].f32);
                    break;
                case 0x8C: // f32.neg
                    vStack[_state.vStackPtr - 1].f32 = -vStack[_state.vStackPtr - 1].f32;
                    break;
                case 0x8D: // f32.ceil
                    vStack[_state.vStackPtr - 1].f32 =
                        (float)Math.Ceiling(vStack[_state.vStackPtr - 1].f32);
                    break;
                case 0x8E: // f32.floor
                    vStack[_state.vStackPtr - 1].f32 =
                        (float)Math.Floor(vStack[_state.vStackPtr - 1].f32);
                    break;
                case 0x8F: // f32.trunc
                    vStack[_state.vStackPtr - 1].f32 =
                        (float)Math.Truncate(vStack[_state.vStackPtr - 1].f32);
                    break;
                case 0x90: // f32.nearest
                    vStack[_state.vStackPtr - 1].f32 =
                        (float)Math.Round(vStack[_state.vStackPtr - 1].f32);
                    break;
                case 0x91: // f32.sqrt
                    vStack[_state.vStackPtr - 1].f32 =
                        (float)Math.Sqrt(vStack[_state.vStackPtr - 1].f32);
                    break;
                case 0x92: // f32.add
                    vStack[_state.vStackPtr - 2].f32 =
                        vStack[_state.vStackPtr - 2].f32 +
                        vStack[_state.vStackPtr - 1].f32;
                    --_state.vStackPtr;
                    break;
                case 0x93: // f32.sub
                    vStack[_state.vStackPtr - 2].f32 =
                        vStack[_state.vStackPtr - 2].f32 -
                        vStack[_state.vStackPtr - 1].f32;
                    --_state.vStackPtr;
                    break;
                case 0x94: // f32.mul
                    vStack[_state.vStackPtr - 2].f32 =
                        vStack[_state.vStackPtr - 2].f32 *
                        vStack[_state.vStackPtr - 1].f32;
                    --_state.vStackPtr;
                    break;
                case 0x95: // f32.div
                    vStack[_state.vStackPtr - 2].f32 =
                        vStack[_state.vStackPtr - 2].f32 /
                        vStack[_state.vStackPtr - 1].f32;
                    --_state.vStackPtr;
                    break;
                case 0x96: // f32.min
                    vStack[_state.vStackPtr - 2].f32 = Math.Min(
                        vStack[_state.vStackPtr - 2].f32,
                        vStack[_state.vStackPtr - 1].f32);
                    --_state.vStackPtr;
                    break;
                case 0x97: // f32.max
                    vStack[_state.vStackPtr - 2].f32 = Math.Max(
                        vStack[_state.vStackPtr - 2].f32,
                        vStack[_state.vStackPtr - 1].f32);
                    --_state.vStackPtr;
                    break;
                case 0x98: // f32.copysign
                    if (vStack[_state.vStackPtr - 2].f32 >= 0 &&
                        vStack[_state.vStackPtr - 1].f32 < 0)
                    {
                        vStack[_state.vStackPtr - 1].f32 =
                            -vStack[_state.vStackPtr - 1].f32;
                    }

                    if (vStack[_state.vStackPtr - 2].f32 < 0 &&
                        vStack[_state.vStackPtr - 1].f32 >= 0)
                    {
                        vStack[_state.vStackPtr - 1].f32 =
                            -vStack[_state.vStackPtr - 1].f32;
                    }

                    --_state.vStackPtr;
                    break;

                case 0x99: // f64.abs
                    vStack[_state.vStackPtr - 1].f64 =
                        Math.Abs(vStack[_state.vStackPtr - 1].f64);
                    break;
                case 0x9A: // f64.neg
                    vStack[_state.vStackPtr - 1].f64 = -vStack[_state.vStackPtr - 1].f64;
                    break;
                case 0x9B: // f64.ceil
                    vStack[_state.vStackPtr - 1].f64 =
                        Math.Ceiling(vStack[_state.vStackPtr - 1].f64);
                    break;
                case 0x9C: // f64.floor
                    vStack[_state.vStackPtr - 1].f64 =
                        Math.Floor(vStack[_state.vStackPtr - 1].f64);
                    break;
                case 0x9D: // f64.trunc
                    vStack[_state.vStackPtr - 1].f64 =
                        Math.Truncate(vStack[_state.vStackPtr - 1].f64);
                    break;
                case 0x9E: // f64.nearest
                    vStack[_state.vStackPtr - 1].f64 =
                        Math.Round(vStack[_state.vStackPtr - 1].f64);
                    break;
                case 0x9F: // f64.sqrt
                    vStack[_state.vStackPtr - 1].f64 =
                        Math.Sqrt(vStack[_state.vStackPtr - 1].f64);
                    break;
                case 0xA0: // f64.add
                    vStack[_state.vStackPtr - 2].f64 =
                        vStack[_state.vStackPtr - 2].f64 +
                        vStack[_state.vStackPtr - 1].f64;
                    --_state.vStackPtr;
                    break;
                case 0xA1: // f64.sub
                    vStack[_state.vStackPtr - 2].f64 =
                        vStack[_state.vStackPtr - 2].f64 -
                        vStack[_state.vStackPtr - 1].f64;
                    --_state.vStackPtr;
                    break;
                case 0xA2: // f64.mul
                    vStack[_state.vStackPtr - 2].f64 =
                        vStack[_state.vStackPtr - 2].f64 *
                        vStack[_state.vStackPtr - 1].f64;
                    --_state.vStackPtr;
                    break;
                case 0xA3: // f64.div
                    vStack[_state.vStackPtr - 2].f64 =
                        vStack[_state.vStackPtr - 2].f64 /
                        vStack[_state.vStackPtr - 1].f64;
                    --_state.vStackPtr;
                    break;
                case 0xA4: // f64.min
                    vStack[_state.vStackPtr - 2].f64 = Math.Min(
                        vStack[_state.vStackPtr - 2].f64,
                        vStack[_state.vStackPtr - 1].f64);
                    --_state.vStackPtr;
                    break;
                case 0xA5: // f64.max
                    vStack[_state.vStackPtr - 2].f64 = Math.Max(
                        vStack[_state.vStackPtr - 2].f64,
                        vStack[_state.vStackPtr - 1].f64);
                    --_state.vStackPtr;
                    break;
                case 0xA6: // f64.copysign
                    if (vStack[_state.vStackPtr - 2].f64 >= 0 &&
                        vStack[_state.vStackPtr - 1].f64 < 0)
                    {
                        vStack[_state.vStackPtr - 1].f64 =
                            -vStack[_state.vStackPtr - 1].f64;
                    }

                    if (vStack[_state.vStackPtr - 2].f64 < 0 &&
                        vStack[_state.vStackPtr - 1].f64 >= 0)
                    {
                        vStack[_state.vStackPtr - 1].f64 =
                            -vStack[_state.vStackPtr - 1].f64;
                    }

                    --_state.vStackPtr;
                    break;

                case 0xA7: // i32.wrap_i64
                    vStack[_state.vStackPtr - 1].type = Type.i32;
                    vStack[_state.vStackPtr - 1].i32 =
                        (uint)vStack[_state.vStackPtr - 1].i64;
                    break;
                case 0xA8: // i32.trunc_f32_s
                    vStack[_state.vStackPtr - 1].type = Type.i32;
                    vStack[_state.vStackPtr - 1].i32 =
                        (uint)(int)Math.Truncate(vStack[_state.vStackPtr - 1].f32);
                    break;
                case 0xA9: // i32.trunc_f32_u
                    vStack[_state.vStackPtr - 1].type = Type.i32;
                    vStack[_state.vStackPtr - 1].i32 =
                        (uint)Math.Truncate(vStack[_state.vStackPtr - 1].f32);
                    break;
                case 0xAA: // i32.trunc_f64_s
                    vStack[_state.vStackPtr - 1].type = Type.i32;
                    vStack[_state.vStackPtr - 1].i32 =
                        (uint)(int)Math.Truncate(vStack[_state.vStackPtr - 1].f64);
                    break;
                case 0xAB: // i32.trunc_f64_u
                    vStack[_state.vStackPtr - 1].type = Type.i32;
                    vStack[_state.vStackPtr - 1].i32 =
                        (uint)Math.Truncate(vStack[_state.vStackPtr - 1].f64);
                    break;
                case 0xAC: // i64.extend_i32_s
                    vStack[_state.vStackPtr - 1].type = Type.i64;
                    vStack[_state.vStackPtr - 1].i64 =
                        (ulong)(int)vStack[_state.vStackPtr - 1].i32;
                    break;
                case 0xAD: // i64.extend_i32_u
                    vStack[_state.vStackPtr - 1].type = Type.i64;
                    vStack[_state.vStackPtr - 1].i64 =
                        vStack[_state.vStackPtr - 1].i32;
                    break;
                case 0xAE: // i64.trunc_f32_s
                    vStack[_state.vStackPtr - 1].type = Type.i64;
                    vStack[_state.vStackPtr - 1].i64 =
                        (ulong)(long)Math.Truncate(vStack[_state.vStackPtr - 1].f32);
                    break;
                case 0xAF: // i64.trunc_f32_u
                    vStack[_state.vStackPtr - 1].type = Type.i64;
                    vStack[_state.vStackPtr - 1].i64 =
                        (ulong)Math.Truncate(vStack[_state.vStackPtr - 1].f32);
                    break;
                case 0xB0: // i64.trunc_f64_s
                    vStack[_state.vStackPtr - 1].type = Type.i64;
                    vStack[_state.vStackPtr - 1].i64 =
                        (ulong)(long)Math.Truncate(vStack[_state.vStackPtr - 1].f64);
                    break;
                case 0xB1: // i64.trunc_f64_u
                    vStack[_state.vStackPtr - 1].type = Type.i64;
                    vStack[_state.vStackPtr - 1].i64 =
                        (ulong)Math.Truncate(vStack[_state.vStackPtr - 1].f64);
                    break;
                case 0xB2: // f32.convert_i32_s
                    vStack[_state.vStackPtr - 1].type = Type.f32;
                    vStack[_state.vStackPtr - 1].f32 =
                        (int)vStack[_state.vStackPtr - 1].i32;
                    break;
                case 0xB3: // f32.convert_i32_u
                    vStack[_state.vStackPtr - 1].type = Type.f32;
                    vStack[_state.vStackPtr - 1].f32 =
                        vStack[_state.vStackPtr - 1].i32;
                    break;
                case 0xB4: // f32.convert_i64_s
                    vStack[_state.vStackPtr - 1].type = Type.f32;
                    vStack[_state.vStackPtr - 1].f32 =
                        (long)vStack[_state.vStackPtr - 1].i64;
                    break;
                case 0xB5: // f32.convert_i64_u
                    vStack[_state.vStackPtr - 1].type = Type.f32;
                    vStack[_state.vStackPtr - 1].f32 =
                        vStack[_state.vStackPtr - 1].i64;
                    break;
                case 0xB6: // f32.demote_f64
                    vStack[_state.vStackPtr - 1].type = Type.f32;
                    vStack[_state.vStackPtr - 1].f32 =
                        (float)vStack[_state.vStackPtr - 1].f64;
                    break;
                case 0xB7: // f64.convert_i32_s
                    vStack[_state.vStackPtr - 1].type = Type.f64;
                    vStack[_state.vStackPtr - 1].f64 =
                        (int)vStack[_state.vStackPtr - 1].i32;
                    break;
                case 0xB8: // f64.convert_i32_u
                    vStack[_state.vStackPtr - 1].type = Type.f64;
                    vStack[_state.vStackPtr - 1].f64 =
                        vStack[_state.vStackPtr - 1].i32;
                    break;
                case 0xB9: // f64.convert_i64_s
                    vStack[_state.vStackPtr - 1].type = Type.f64;
                    vStack[_state.vStackPtr - 1].f64 =
                        (long)vStack[_state.vStackPtr - 1].i64;
                    break;
                case 0xBA: // f64.convert_i64.u
                    vStack[_state.vStackPtr - 1].type = Type.f64;
                    vStack[_state.vStackPtr - 1].f64 =
                        vStack[_state.vStackPtr - 1].i64;
                    break;
                case 0xBB: // f64.promote_f32
                    vStack[_state.vStackPtr - 1].type = Type.f64;
                    vStack[_state.vStackPtr - 1].f64 =
                        vStack[_state.vStackPtr - 1].f32;
                    break;
                case 0xBC: // i32.reinterpret_f32
                    vStack[_state.vStackPtr - 1].type = Type.i32;
                    break;
                case 0xBD: // i64.reinterpret_f64
                    vStack[_state.vStackPtr - 1].type = Type.i64;
                    break;
                case 0xBE: // f32.reinterpret_i32
                    vStack[_state.vStackPtr - 1].type = Type.f32;
                    break;
                case 0xBF: // f64.reinterpret_i64
                    vStack[_state.vStackPtr - 1].type = Type.f64;
                    break;


                /* OPTIMIZED OPCODES */

                case 0x200D: // local.br_if
                    if (_state.locals[_state.program[_state.ip].a].i32 > 0)
                    {
                        if (_state.labelPtr - _state.program[_state.ip].pos >= 0)
                        {
                            label = _state.lStack[_state.labelPtr - _state.program[_state.ip].pos];
                            if (!(label.i is Loop) && label.i.type != 0x40)
                            {
                                vStack[label.vStackPtr] = vStack[--_state.vStackPtr];
                                label.vStackPtr++;
                            }

                            _state.labelPtr -= _state.program[_state.ip].pos;
                            _state.vStackPtr = _state.lStack[_state.labelPtr].vStackPtr;

                            _state.ip = _state.lStack[_state.labelPtr].ip;
                        }
                        else
                        {
                            _state.ip = _state.program.Length - 2;
                        }
                    }
                    else
                    {
                        ++_state.ip;
                    }

                    break;
                case 0x2021: // local.copy
                    _state.locals[_state.program[_state.ip].b] = _state.locals[_state.program[_state.ip].a];
                    ++_state.ip;
                    break;
                case 0x2028: // local.i32.load
                case 0x202A: // local.f32.load
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    vStack[_state.vStackPtr].b0 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b1 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b2 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b3 = _state.memory.Buffer[offset];
                    ++_state.ip;
                    _state.vStackPtr++;
                    break;
                case 0x2029: // local.i64.load
                case 0x202B: // local.f64.load
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    vStack[_state.vStackPtr].b0 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b1 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b2 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b3 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b4 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b5 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b6 = _state.memory.Buffer[offset];
                    ++offset;
                    vStack[_state.vStackPtr].b7 = _state.memory.Buffer[offset];
                    ++_state.ip;
                    _state.vStackPtr++;
                    break;

                case 0x202C: // local.i32.load8_s
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    vStack[_state.vStackPtr].i32 = (uint)(sbyte)_state.memory.Buffer[offset];
                    ++_state.ip;
                    _state.vStackPtr++;
                    break;
                case 0x202D: // local.i32.load8_u
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    vStack[_state.vStackPtr].i32 = _state.memory.Buffer[offset];
                    ++_state.ip;
                    _state.vStackPtr++;
                    break;
                case 0x202E: // local.i32.load16_s
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    vStack[_state.vStackPtr].i32 =
                        (uint)(short)(_state.memory.Buffer[offset] | (ushort)(_state.memory.Buffer[offset + 1] << 8));
                    ++_state.ip;
                    _state.vStackPtr++;
                    break;
                case 0x202F: // local.i32.load16_u
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    vStack[_state.vStackPtr].i32 = _state.memory.Buffer[offset] | (uint)(_state.memory.Buffer[offset + 1] << 8);
                    ++_state.ip;
                    _state.vStackPtr++;
                    break;

                case 0x2036: // local.i32.store
                case 0x2038: // local.f32.store
                    --_state.vStackPtr;
                    offset = _state.program[_state.ip].pos64 + vStack[_state.vStackPtr].i32;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b0;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b1;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b2;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b3;
                    ++_state.ip;
                    break;
                case 0x2037: // local.i64.store
                case 0x2039: // local.f64.store
                    --_state.vStackPtr;
                    offset = _state.program[_state.ip].pos64 + vStack[_state.vStackPtr].i32;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b0;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b1;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b2;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b3;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b4;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b5;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b6;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b7;
                    ++_state.ip;
                    break;

                case 0x203A: // local.i32.store8
                    --_state.vStackPtr;
                    offset = _state.program[_state.ip].pos64 + vStack[_state.vStackPtr].i32;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b0;
                    ++_state.ip;
                    break;
                case 0x203B: // local.i32.store16
                    --_state.vStackPtr;
                    offset = _state.program[_state.ip].pos64 + vStack[_state.vStackPtr].i32;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b0;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].a].b1;
                    ++_state.ip;
                    break;

                case 0x2045: // local.i32.eqz
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 == 0 ? 1 : (uint)0;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x2046: // local.i32.eq
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 == vStack[_state.vStackPtr].i32 ? 1 : (uint)0;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x2047: // local.i32.ne
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 != vStack[_state.vStackPtr].i32 ? 1 : (uint)0;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x2048: // local.i32.lt_s
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = (int)vStack[_state.vStackPtr].i32 < (int)_state.locals[_state.program[_state.ip].a].i32
                        ? 1
                        : (uint)0;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x2049: // local.i32.lt_u
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = vStack[_state.vStackPtr].i32 < _state.locals[_state.program[_state.ip].a].i32 ? 1 : (uint)0;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x204A: // local.i32.gt_s
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = (int)vStack[_state.vStackPtr].i32 > (int)_state.locals[_state.program[_state.ip].a].i32
                        ? 1
                        : (uint)0;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x204B: // local.i32.gt_u
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = vStack[_state.vStackPtr].i32 > _state.locals[_state.program[_state.ip].a].i32 ? 1 : (uint)0;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x206A: // local.i32.add
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 + vStack[_state.vStackPtr].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x206B: // local.i32.sub
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = vStack[_state.vStackPtr].i32 - _state.locals[_state.program[_state.ip].a].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x206C: // local.i32.mul
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 * vStack[_state.vStackPtr].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x206D: // local.i32.div_s
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 =
                        (uint)((int)vStack[_state.vStackPtr].i32 / (int)_state.locals[_state.program[_state.ip].a].i32);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x206E: // local.i32.div_u
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = vStack[_state.vStackPtr].i32 / _state.locals[_state.program[_state.ip].a].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x206F: // local.i32.rem_s
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 =
                        (uint)((vStack[_state.vStackPtr].i32 == 0x80000000) & (_state.locals[_state.program[_state.ip].a].i32 == 0xFFFFFFFF)
                            ? 0
                            : (int)vStack[_state.vStackPtr].i32 % (int)_state.locals[_state.program[_state.ip].a].i32);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x2070: // local.i32.rem_u
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = vStack[_state.vStackPtr].i32 % _state.locals[_state.program[_state.ip].a].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x2071: // local.i32.and
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 & vStack[_state.vStackPtr].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x2072: // local.i32.or
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 | vStack[_state.vStackPtr].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x2073: // local.i32.xor
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 ^ vStack[_state.vStackPtr].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x2074: // local.i32.shl
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = vStack[_state.vStackPtr].i32 << (int)_state.locals[_state.program[_state.ip].a].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x2075: // local.i32.shr_s
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 =
                        (uint)((int)vStack[_state.vStackPtr].i32 >> (int)_state.locals[_state.program[_state.ip].a].i32);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x2076: // local.i32.shr_u
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].i32 = vStack[_state.vStackPtr].i32 >> (int)_state.locals[_state.program[_state.ip].a].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;

                case 0x2099: // local.f64.abs
                    vStack[_state.vStackPtr].f64 = Math.Abs(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x209A: // local.f64.neg
                    vStack[_state.vStackPtr].f64 = -_state.locals[_state.program[_state.ip].a].f64;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x209B: // local.f64.ceil
                    vStack[_state.vStackPtr].f64 = Math.Ceiling(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x209C: // local.f64.floor
                    vStack[_state.vStackPtr].f64 = Math.Floor(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x209D: // local.f64.trunc
                    vStack[_state.vStackPtr].f64 = Math.Truncate(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x209E: // local.f64.nearest
                    vStack[_state.vStackPtr].f64 = Math.Round(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x209F: // local.f64.sqrt
                    vStack[_state.vStackPtr].f64 = Math.Sqrt(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x20A0: // local.f64.add
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].f64 = vStack[_state.vStackPtr].f64 + _state.locals[_state.program[_state.ip].a].f64;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x20A1: // local.f64.sub
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].f64 = vStack[_state.vStackPtr].f64 - _state.locals[_state.program[_state.ip].a].f64;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x20A2: // local.f64.mul
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].f64 = vStack[_state.vStackPtr].f64 * _state.locals[_state.program[_state.ip].a].f64;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x20A3: // local.f64.div
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].f64 = vStack[_state.vStackPtr].f64 / _state.locals[_state.program[_state.ip].a].f64;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x20A4: // local.f64.min
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].f64 = Math.Min(vStack[_state.vStackPtr].f64, _state.locals[_state.program[_state.ip].a].f64);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x20A5: // local.f64.max
                    --_state.vStackPtr;
                    vStack[_state.vStackPtr].f64 = Math.Max(vStack[_state.vStackPtr].f64, _state.locals[_state.program[_state.ip].a].f64);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x20A6: // local.f64.copysign
                    --_state.vStackPtr;
                    if (vStack[_state.vStackPtr].f64 >= 0 && _state.locals[_state.program[_state.ip].a].f64 < 0)
                    {
                        vStack[_state.vStackPtr].f64 = -_state.locals[_state.program[_state.ip].a].f64;
                    }

                    if (vStack[_state.vStackPtr].f64 < 0 && _state.locals[_state.program[_state.ip].a].f64 >= 0)
                    {
                        vStack[_state.vStackPtr].f64 = -_state.locals[_state.program[_state.ip].a].f64;
                    }

                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;

                case 0x20B7: // local.f64.convert_i32_s
                    vStack[_state.vStackPtr].type = Type.f64;
                    vStack[_state.vStackPtr].f64 = (int)_state.locals[_state.program[_state.ip].a].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;
                case 0x20B8: // local.f64.convert_i32_u
                    vStack[_state.vStackPtr].type = Type.f64;
                    vStack[_state.vStackPtr].f64 = _state.locals[_state.program[_state.ip].a].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    break;

                case 0xB721: // f64.convert_i32_s.local
                    --_state.vStackPtr;
                    _state.locals[_state.program[_state.ip].a].type = Type.f64;
                    _state.locals[_state.program[_state.ip].a].f64 = (int)vStack[_state.vStackPtr].i32;
                    ++_state.ip;
                    break;
                case 0xB821: // f64.convert_i32_u.local
                    --_state.vStackPtr;
                    _state.locals[_state.program[_state.ip].a].type = Type.f64;
                    _state.locals[_state.program[_state.ip].a].f64 = vStack[_state.vStackPtr].i32;
                    ++_state.ip;
                    break;

                case 0x4121: // i32.const.local
                    _state.locals[_state.program[_state.ip].a].i32 = _state.program[_state.ip].i32;
                    ++_state.ip;
                    break;
                case 0x4221: // i64.const.local
                    _state.locals[_state.program[_state.ip].a].i64 = _state.program[_state.ip].i64;
                    ++_state.ip;
                    break;
                case 0x202036: // local.local.i32.store
                case 0x202038: // local.local.f32.store
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b0;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b1;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b2;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b3;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202037: // local.local.i64.store
                case 0x202039: // local.local.f64.store
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b0;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b1;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b2;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b3;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b4;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b5;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b6;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b7;
                    ++_state.ip;
                    ++_state.ip;
                    break;

                case 0x20203A: // local.local.i32.store8
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b0;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20203B: // local.local.i32.store16
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b0;
                    ++offset;
                    _state.memory.Buffer[offset] = _state.locals[_state.program[_state.ip].b].b1;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202046: // local.local.132.eq
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 =
                        (uint)(_state.locals[_state.program[_state.ip].a].i32 == _state.locals[_state.program[_state.ip].b].i32 ? 1 : 0);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202047: // local.local.132.ne
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 =
                        (uint)(_state.locals[_state.program[_state.ip].a].i32 != _state.locals[_state.program[_state.ip].b].i32 ? 1 : 0);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202048: // local.local.i32.lt_s
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 =
                        (uint)((int)_state.locals[_state.program[_state.ip].a].i32 < (int)_state.locals[_state.program[_state.ip].b].i32 ? 1 : 0);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202049: // local.local.i32.lt_u
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 =
                        _state.locals[_state.program[_state.ip].a].i32 < _state.locals[_state.program[_state.ip].b].i32 ? 1 : (uint)0;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20204A: // local.local.i32.gt_s
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 =
                        (uint)((int)_state.locals[_state.program[_state.ip].a].i32 > (int)_state.locals[_state.program[_state.ip].b].i32 ? 1 : 0);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20204B: // local.local.i32.gt_u
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 =
                        _state.locals[_state.program[_state.ip].a].i32 > _state.locals[_state.program[_state.ip].b].i32 ? 1 : (uint)0;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20206A: // local.local.132.add
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 + _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20206B: // local.local.132.sub
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 - _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20206C: // local.local.132.mul
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 * _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20206D: // local.local.132.div_s
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 =
                        (uint)((int)_state.locals[_state.program[_state.ip].a].i32 / (int)_state.locals[_state.program[_state.ip].b].i32);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20206E: // local.local.132.div_u
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 / _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20206F: // local.local.132.rem_s
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 =
                        (uint)(_state.locals[_state.program[_state.ip].a].i32 == 0x80000000 &&
                               _state.locals[_state.program[_state.ip].b].i32 == 0xFFFFFFFF
                            ? 0
                            : (int)_state.locals[_state.program[_state.ip].a].i32 % (int)_state.locals[_state.program[_state.ip].b].i32);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202070: // local.local.132.rem_u
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 % _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202071: // local.local.i32.and
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 & _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202072: // local.local.i32.or
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 | _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202073: // local.local.i32.xor
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 ^ _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202074: // local.local.i32.shl
                    vStack[_state.vStackPtr].type = Type.i32;
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 << (int)_state.locals[_state.program[_state.ip].b].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202075: // local.local.i32.shr_s
                    vStack[_state.vStackPtr].i32 =
                        (uint)((int)_state.locals[_state.program[_state.ip].a].i32 >> (int)_state.locals[_state.program[_state.ip].b].i32);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202076: // local.local.i32.shr_u
                    vStack[_state.vStackPtr].i32 = _state.locals[_state.program[_state.ip].a].i32 >> (int)_state.locals[_state.program[_state.ip].b].i32;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;

                case 0x2020A0: // local.local.f64.add
                    vStack[_state.vStackPtr].f64 = _state.locals[_state.program[_state.ip].a].f64 + _state.locals[_state.program[_state.ip].b].f64;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x2020A1: // local.local.f64.sub
                    vStack[_state.vStackPtr].f64 = _state.locals[_state.program[_state.ip].a].f64 - _state.locals[_state.program[_state.ip].b].f64;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x2020A2: // local.local.f64.mul
                    vStack[_state.vStackPtr].f64 = _state.locals[_state.program[_state.ip].a].f64 * _state.locals[_state.program[_state.ip].b].f64;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x2020A3: // local.local.f64.div
                    vStack[_state.vStackPtr].f64 = _state.locals[_state.program[_state.ip].a].f64 / _state.locals[_state.program[_state.ip].b].f64;
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x2020A4: // local.local.f64.min
                    vStack[_state.vStackPtr].f64 =
                        Math.Min(_state.locals[_state.program[_state.ip].a].f64, _state.locals[_state.program[_state.ip].b].f64);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x2020A5: // local.local.f64.max
                    vStack[_state.vStackPtr].f64 =
                        Math.Max(_state.locals[_state.program[_state.ip].a].f64, _state.locals[_state.program[_state.ip].b].f64);
                    ++_state.vStackPtr;
                    ++_state.ip;
                    ++_state.ip;
                    break;

                case 0x202821: // local.i32.load.local
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    index = _state.program[_state.ip].b;
                    _state.locals[index].b0 = _state.memory.Buffer[offset];
                    ++offset;
                    _state.locals[index].b1 = _state.memory.Buffer[offset];
                    ++offset;
                    _state.locals[index].b2 = _state.memory.Buffer[offset];
                    ++offset;
                    _state.locals[index].b3 = _state.memory.Buffer[offset];
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202921: // local.i64.load.local
                case 0x202B21: // local.f64.load.local
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    _state.locals[_state.program[_state.ip].b].b0 = _state.memory.Buffer[offset];
                    ++offset;
                    _state.locals[_state.program[_state.ip].b].b1 = _state.memory.Buffer[offset];
                    ++offset;
                    _state.locals[_state.program[_state.ip].b].b2 = _state.memory.Buffer[offset];
                    ++offset;
                    _state.locals[_state.program[_state.ip].b].b3 = _state.memory.Buffer[offset];
                    ++offset;
                    _state.locals[_state.program[_state.ip].b].b4 = _state.memory.Buffer[offset];
                    ++offset;
                    _state.locals[_state.program[_state.ip].b].b5 = _state.memory.Buffer[offset];
                    ++offset;
                    _state.locals[_state.program[_state.ip].b].b6 = _state.memory.Buffer[offset];
                    ++offset;
                    _state.locals[_state.program[_state.ip].b].b7 = _state.memory.Buffer[offset];
                    ++_state.ip;
                    ++_state.ip;
                    break;

                case 0x202C21: // local.i32.load8_s.local
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    _state.locals[_state.program[_state.ip].b].i32 = (uint)(sbyte)_state.memory.Buffer[offset];
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202D21: // local.i32.load8_u.local
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    _state.locals[_state.program[_state.ip].b].i32 = _state.memory.Buffer[offset];
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202E21: // local.i32.load16_s.local
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    _state.locals[_state.program[_state.ip].b].i32 =
                        (uint)(short)(_state.memory.Buffer[offset] | (ushort)(_state.memory.Buffer[offset + 1] << 8));
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x202F21: // local.i32.load16_u.local
                    offset = _state.program[_state.ip].pos64 + _state.locals[_state.program[_state.ip].a].i32;
                    _state.locals[_state.program[_state.ip].b].i32 =
                        _state.memory.Buffer[offset] | (uint)(_state.memory.Buffer[offset + 1] << 8);
                    ++_state.ip;
                    ++_state.ip;
                    break;

                case 0x209921: // local.f64.abs.local
                    _state.locals[_state.program[_state.ip].b].f64 = Math.Abs(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x209A21: // local.f64.neg.local
                    _state.locals[_state.program[_state.ip].b].f64 = -_state.locals[_state.program[_state.ip].a].f64;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x209B21: // local.f64.ceil.local
                    _state.locals[_state.program[_state.ip].b].f64 = Math.Ceiling(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x209C21: // local.f64.floor.local
                    _state.locals[_state.program[_state.ip].b].f64 = Math.Floor(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x209D21: // local.f64.trunc.local
                    _state.locals[_state.program[_state.ip].b].f64 = Math.Truncate(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x209E21: // local.f64.nearest.local
                    _state.locals[_state.program[_state.ip].b].f64 = Math.Round(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x209F21: // local.f64.sqrt.local
                    _state.locals[_state.program[_state.ip].b].f64 = Math.Sqrt(_state.locals[_state.program[_state.ip].a].f64);
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20A021: // local.f64.add.local
                    --_state.vStackPtr;
                    _state.locals[_state.program[_state.ip].b].f64 = vStack[_state.vStackPtr].f64 + _state.locals[_state.program[_state.ip].a].f64;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20A121: // local.f64.sub.local
                    --_state.vStackPtr;
                    _state.locals[_state.program[_state.ip].b].f64 = vStack[_state.vStackPtr].f64 - _state.locals[_state.program[_state.ip].a].f64;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20A221: // local.f64.mul.local
                    --_state.vStackPtr;
                    _state.locals[_state.program[_state.ip].b].f64 = vStack[_state.vStackPtr].f64 * _state.locals[_state.program[_state.ip].a].f64;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20A321: // local.f64.div.local
                    --_state.vStackPtr;
                    _state.locals[_state.program[_state.ip].b].f64 = vStack[_state.vStackPtr].f64 / _state.locals[_state.program[_state.ip].a].f64;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20A421: // local.f64.min.local
                    --_state.vStackPtr;
                    _state.locals[_state.program[_state.ip].b].f64 =
                        Math.Min(vStack[_state.vStackPtr].f64, _state.locals[_state.program[_state.ip].a].f64);
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20A521: // local.f64.max.local
                    --_state.vStackPtr;
                    _state.locals[_state.program[_state.ip].b].f64 =
                        Math.Max(vStack[_state.vStackPtr].f64, _state.locals[_state.program[_state.ip].a].f64);
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20A621: // local.f64.copysign.local
                    --_state.vStackPtr;
                    if (vStack[_state.vStackPtr].f64 >= 0 && _state.locals[_state.program[_state.ip].a].f64 < 0)
                    {
                        _state.locals[_state.program[_state.ip].b].f64 = -_state.locals[_state.program[_state.ip].a].f64;
                    }

                    if (vStack[_state.vStackPtr].f64 < 0 && _state.locals[_state.program[_state.ip].a].f64 >= 0)
                    {
                        _state.locals[_state.program[_state.ip].b].f64 = -_state.locals[_state.program[_state.ip].a].f64;
                    }

                    ++_state.ip;
                    ++_state.ip;
                    break;

                case 0x20B721: // local.f64.convert_i32_s.local
                    _state.locals[_state.program[_state.ip].b].type = Type.f64;
                    _state.locals[_state.program[_state.ip].b].f64 = (int)_state.locals[_state.program[_state.ip].a].i32;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20B821: // local.f64.convert_i32_u.local
                    _state.locals[_state.program[_state.ip].b].type = Type.f64;
                    _state.locals[_state.program[_state.ip].b].f64 = _state.locals[_state.program[_state.ip].a].i32;
                    ++_state.ip;
                    ++_state.ip;
                    break;


                /* 32-bit */

                case 0x20204621: // local.local.i32.eq.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 =
                        (uint)(_state.locals[_state.program[_state.ip].a].i32 == _state.locals[_state.program[_state.ip].b].i32 ? 1 : 0);
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20204721: // local.local.i32.ne.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 =
                        (uint)(_state.locals[_state.program[_state.ip].a].i32 != _state.locals[_state.program[_state.ip].b].i32 ? 1 : 0);
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20204821: // local.local.i32.lt_s.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 =
                        (uint)((int)_state.locals[_state.program[_state.ip].a].i32 < (int)_state.locals[_state.program[_state.ip].b].i32 ? 1 : 0);
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20204921: // local.local.i32.lt_u.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 = _state.locals[_state.program[_state.ip].a].i32 < _state.locals[_state.program[_state.ip].b].i32
                        ? 1
                        : (uint)0;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20204A21: // local.local.i32.gt_s.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 =
                        (uint)((int)_state.locals[_state.program[_state.ip].a].i32 > (int)_state.locals[_state.program[_state.ip].b].i32 ? 1 : 0);
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20204B21: // local.local.i32.gt_u.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 = _state.locals[_state.program[_state.ip].a].i32 > _state.locals[_state.program[_state.ip].b].i32
                        ? 1
                        : (uint)0;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;

                case 0x20206A21: // local.local.i32.add.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 = _state.locals[_state.program[_state.ip].a].i32 + _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20206B21: // local.local.i32.sub.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 = _state.locals[_state.program[_state.ip].a].i32 - _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20206C21: // local.local.i32.mul.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 = _state.locals[_state.program[_state.ip].a].i32 * _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20206D21: // local.local.i32.div_s.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 =
                        (uint)((int)_state.locals[_state.program[_state.ip].a].i32 / (int)_state.locals[_state.program[_state.ip].b].i32);
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20206E21: // local.local.i32.div_u.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 = _state.locals[_state.program[_state.ip].a].i32 / _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20206F21: // local.local.i32.rem_s.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 =
                        (uint)(_state.locals[_state.program[_state.ip].a].i32 == 0x80000000 &&
                               _state.locals[_state.program[_state.ip].b].i32 == 0xFFFFFFFF
                            ? 0
                            : (int)_state.locals[_state.program[_state.ip].a].i32 % (int)_state.locals[_state.program[_state.ip].b].i32);
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207021: // local.local.i32.rem_u.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 = _state.locals[_state.program[_state.ip].a].i32 - _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207121: // local.local.i32.and.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 = _state.locals[_state.program[_state.ip].a].i32 & _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207221: // local.local.i32.or.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 = _state.locals[_state.program[_state.ip].a].i32 | _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207321: // local.local.i32.xor.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 = _state.locals[_state.program[_state.ip].a].i32 ^ _state.locals[_state.program[_state.ip].b].i32;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207421: // local.local.i32.shl.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 =
                        _state.locals[_state.program[_state.ip].a].i32 << (int)_state.locals[_state.program[_state.ip].b].i32;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207521: // local.local.i32.shr_s.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 =
                        (uint)((int)_state.locals[_state.program[_state.ip].a].i32 >> (int)_state.locals[_state.program[_state.ip].b].i32);
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207621: // local.local.i32.shr_u.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 =
                        _state.locals[_state.program[_state.ip].a].i32 >> (int)_state.locals[_state.program[_state.ip].b].i32;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207721: // local.local.i32.rotl.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 =
                        (_state.locals[_state.program[_state.ip].a].i32 << (int)_state.locals[_state.program[_state.ip].b].i32) |
                        (_state.locals[_state.program[_state.ip].a].i32 >> (32 - (int)_state.locals[_state.program[_state.ip].b].i32));
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207821: // local.local.i32.rotr.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i32;
                    _state.locals[_state.program[_state.ip].c].i32 =
                        (_state.locals[_state.program[_state.ip].a].i32 >> (int)_state.locals[_state.program[_state.ip].b].i32) |
                        (_state.locals[_state.program[_state.ip].a].i32 << (32 - (int)_state.locals[_state.program[_state.ip].b].i32));
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;

                /* 64-bit */

                case 0x20207C21: // local.local.i64.add.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 = _state.locals[_state.program[_state.ip].a].i64 + _state.locals[_state.program[_state.ip].b].i64;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207D21: // local.local.i64.sub.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 = _state.locals[_state.program[_state.ip].a].i64 - _state.locals[_state.program[_state.ip].b].i64;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207E21: // local.local.i64.mul.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 = _state.locals[_state.program[_state.ip].a].i64 * _state.locals[_state.program[_state.ip].b].i64;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20207F21: // local.local.i64.div_s.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 =
                        (ulong)((long)_state.locals[_state.program[_state.ip].a].i64 / (long)_state.locals[_state.program[_state.ip].b].i64);
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20208021: // local.local.i64.div_u.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 = _state.locals[_state.program[_state.ip].a].i64 / _state.locals[_state.program[_state.ip].b].i64;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20208121: // local.local.i64.rem_s.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 =
                        (ulong)(_state.locals[_state.program[_state.ip].a].i64 == 0x8000000000000000 &&
                                _state.locals[_state.program[_state.ip].b].i64 == 0xFFFFFFFFFFFFFFFF
                            ? 0
                            : (long)_state.locals[_state.program[_state.ip].a].i64 % (long)_state.locals[_state.program[_state.ip].b].i64);
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20208221: // local.local.i64.rem_u.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 = _state.locals[_state.program[_state.ip].a].i64 % _state.locals[_state.program[_state.ip].b].i64;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20208321: // local.local.i64.and.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 = _state.locals[_state.program[_state.ip].a].i64 & _state.locals[_state.program[_state.ip].b].i64;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20208421: // local.local.i64.or.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 = _state.locals[_state.program[_state.ip].a].i64 | _state.locals[_state.program[_state.ip].b].i64;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20208521: // local.local.i64.xor.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 = _state.locals[_state.program[_state.ip].a].i64 ^ _state.locals[_state.program[_state.ip].b].i64;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20208621: // local.local.i64.shl.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 =
                        _state.locals[_state.program[_state.ip].a].i64 << (int)_state.locals[_state.program[_state.ip].b].i64;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20208721: // local.local.i64.shr_s.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 =
                        (ulong)((long)_state.locals[_state.program[_state.ip].a].i64 >> (int)_state.locals[_state.program[_state.ip].b].i64);
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20208821: // local.local.i64.shr_u.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 =
                        _state.locals[_state.program[_state.ip].a].i64 >> (int)_state.locals[_state.program[_state.ip].b].i64;
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20208921: // local.local.i64.rotl.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 =
                        (_state.locals[_state.program[_state.ip].a].i64 << (int)_state.locals[_state.program[_state.ip].b].i64) |
                        (_state.locals[_state.program[_state.ip].a].i64 >> (64 - (int)_state.locals[_state.program[_state.ip].b].i64));
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0x20208A21: // local.local.i64.rotr.local
                    _state.locals[_state.program[_state.ip].c].type = Type.i64;
                    _state.locals[_state.program[_state.ip].c].i64 =
                        (_state.locals[_state.program[_state.ip].a].i64 >> (int)_state.locals[_state.program[_state.ip].b].i64) |
                        (_state.locals[_state.program[_state.ip].a].i64 << (64 - (int)_state.locals[_state.program[_state.ip].b].i64));
                    ++_state.ip;
                    ++_state.ip;
                    ++_state.ip;
                    break;
                case 0xFF000000: // loop of local.i32.load.local
                    length = _state.ip;
                    for (i = 0; i < _state.program[length].optimalProgram.Length; ++i)
                    {
                        offset = _state.program[length].optimalProgram[i].pos64 +
                                 _state.locals[_state.program[length].optimalProgram[i].a].i32;
                        index = _state.program[length].optimalProgram[i].b;
                        _state.locals[index].b0 = _state.memory.Buffer[offset];
                        ++offset;
                        _state.locals[index].b1 = _state.memory.Buffer[offset];
                        ++offset;
                        _state.locals[index].b2 = _state.memory.Buffer[offset];
                        ++offset;
                        _state.locals[index].b3 = _state.memory.Buffer[offset];
                        ++_state.ip;
                        ++_state.ip;
                        ++_state.ip;
                    }

                    --_state.ip;
                    break;
                case 0xFE000000: // loop of i32.const.local
                    index = _state.ip;
                    for (i = 0; i < _state.program[index].optimalProgram.Length; ++i)
                    {
                        _state.locals[_state.program[index].optimalProgram[i].a].i32 = _state.program[index].optimalProgram[i].i32;
                        ++_state.ip;
                        ++_state.ip;
                    }

                    --_state.ip;
                    break;
                default:
                    throw new Exception("Invalid opCode: " + _state.program[_state.ip].opCode.ToString("X"));
            }


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
