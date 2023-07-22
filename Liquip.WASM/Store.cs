using System.Collections.Generic;
using System.IO;
using Liquip.WASM.VM;

namespace Liquip.WASM;

public class Store
{
    public Dictionary<string, BaseModule> Modules = new();

    public Runtime runtime = new();

    public BaseModule LoadModule(string name, string fileName)
    {
        return LoadModule(name, File.ReadAllBytes(fileName));
    }

    public BaseModule LoadModule(string name, byte[] bytes)
    {
        return LoadModule(new BaseModule(name, this, bytes));
    }

    public BaseModule LoadModule(BaseModule baseModule)
    {
        Modules.Add(baseModule.Name, baseModule);
        return baseModule;
    }

    // Returning false means execution is complete
    public bool Step(int? count)
    {
        return runtime.Step(count);
    }
}
