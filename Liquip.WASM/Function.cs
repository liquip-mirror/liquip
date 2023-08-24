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

    /// <summary>
    /// WASM function constructor
    /// </summary>
    /// <param name="baseModule"></param>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <param name="index"></param>
    /// <param name="start"></param>
    public Function(BaseModule baseModule, string name, Type? type = null, uint index = 0xFFFFFFFF,
        Instruction.Instruction? start = null)
    {
        BaseModule = baseModule;
        Name = name;
        Index = index;
        GlobalIndex = BaseModule.Host.Runtime.AddFunction(this);

        Type = type ?? new Type(Array.Empty<byte>(), Array.Empty<byte>());
    }

    /// <summary>
    /// Native function constructor
    /// </summary>
    /// <param name="baseModule"></param>
    /// <param name="name"></param>
    /// <param name="action"></param>
    /// <param name="type"></param>
    public Function(BaseModule baseModule, string name, Func<Value[], Value[]> action, Type type)
    {
        BaseModule = baseModule;
        Name = name;
        Type = type;
        GlobalIndex = BaseModule.Host.Runtime.AddFunction(this);
        native = action;
    }

    /// <summary>
    ///
    /// </summary>
    /// <exception cref="Exception"></exception>
    protected void NotImplemented()
    {
        throw new Exception("Function not implemented: " + BaseModule.Name + "@" + Name);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    public void SetName(string name)
    {
        Name = name;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return Name;
    }
}
