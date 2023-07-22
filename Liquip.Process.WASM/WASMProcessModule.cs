using Liquip.WASM;
using Type = Liquip.WASM.Type;

namespace Liquip.Process.WASM;

public class WASMProcessModule: BaseModule
{
    private readonly WASMProcess _process;

    public WASMProcessModule(WASMProcess process, Store store) : base(nameof(WASMProcessModule), store)
    {
        _process = process;
    }

    public WASMProcessModule(WASMProcess process, Store store, byte[] bytes) : base(nameof(WASMProcessModule), store, bytes)
    {
        _process = process;
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
