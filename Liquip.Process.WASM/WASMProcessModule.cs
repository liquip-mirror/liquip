using Liquip.WASM;
using Liquip.WASM.VM;
using Type = Liquip.WASM.Type;

namespace Liquip.Process.WASM;

public class WASMProcessModule: BaseModule
{
    private readonly WASMProcess _process;

    public WASMProcessModule(WASMProcess process, Host host) : base(nameof(WASMProcessModule), host)
    {
        _process = process;
        RegisterMethods();
    }

    public WASMProcessModule(WASMProcess process, Host host, byte[] bytes) : base(nameof(WASMProcessModule), host, bytes)
    {
        _process = process;
        RegisterMethods();
    }

    public void RegisterMethods()
    {
        AddExportFunc(nameof(WorkingDir), null, new[] { Type.i32 }, WorkingDir);
    }

    Value[] WorkingDir(Value[] args)
    {
        return new Value[] { Value.From(0) };
    }
}
