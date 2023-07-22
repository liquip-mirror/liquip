using System;
using Liquip.WASM.VM;

namespace Liquip.WASM;

public class Function
{
    public readonly int GlobalIndex;
    public uint Index;

    public Value[] LocalTypes = Array.Empty<Value>();
    public BaseModule BaseModule { get; init; }
    public string Name;
    public Func<Value[], Value[]>? native;
    public Inst[]? program = null;
    public Type Type;

    // Standard constructor
    public Function(BaseModule baseModule, string name, Type? type = null, uint index = 0xFFFFFFFF,
        Instruction.Instruction? start = null)
    {
        BaseModule = baseModule;
        Name = name;
        Index = index;
        GlobalIndex = BaseModule.Store.runtime.AddFunction(this);

        Type = type ?? new Type(Array.Empty<byte>(), Array.Empty<byte>());
    }

    // Native function constructor
    public Function(BaseModule baseModule, string name, Func<Value[], Value[]> action, Type type)
    {
        BaseModule = baseModule;
        Name = name;
        Type = type;
        GlobalIndex = BaseModule.Store.runtime.AddFunction(this);
        native = action;
    }

    protected void NotImplemented()
    {
        throw new Exception("Function not implemented: " + BaseModule.Name + "@" + Name);
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public string GetName()
    {
        return Name;
    }
}
