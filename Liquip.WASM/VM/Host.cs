namespace Liquip.WASM.VM;

public class Host
{
    private readonly Store _store;

    public Host()
    {
        _store = new Store();
    }

    public BaseModule LoadModule(string name, string fileName)
    {
        return _store.LoadModule(name, fileName);
    }

    public BaseModule LoadModule(string name, byte[] bytes)
    {
        return _store.LoadModule(name, bytes);
    }

    public BaseModule LoadModule(BaseModule baseModule)
    {
        return _store.LoadModule(baseModule);
    }

    public bool Step() => Step(null);

    public bool Step(int? count)
    {
        return _store.Step(count);
    }
}
