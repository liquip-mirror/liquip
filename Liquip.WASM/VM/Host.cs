namespace Liquip.WASM.VM;

public class Host
{

    /// <summary>
    ///
    /// </summary>
    public readonly Dictionary<string, BaseModule> Modules = new();

    /// <summary>
    /// the VM
    /// </summary>
    public readonly Runtime Runtime = new();

    public Host()
    {

    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public BaseModule LoadModule(string name, string fileName)
    {
        return LoadModule(name, File.ReadAllBytes(fileName));
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public BaseModule LoadModule(string name, byte[] bytes)
    {
        return LoadModule(new BaseModule(name, this, bytes));
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="baseModule"></param>
    /// <returns></returns>
    public BaseModule LoadModule(BaseModule baseModule)
    {
        Modules.Add(baseModule.Name, baseModule);
        return baseModule;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="method"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public Value Call(string method, params object[] args)
    {

        Step();
        return Runtime.ReturnValue();
    }

    /// <summary>
    /// step until end
    /// </summary>
    public void Step() => Step(null);


    /// <summary>
    /// step with a limit
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool Step(int? count)
    {
        return Runtime.Step(count);
    }
}
