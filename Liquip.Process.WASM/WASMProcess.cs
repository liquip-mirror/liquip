using System;
using System.IO;
using Liquip.Common;
using Liquip.WASM.Interfaces;
using Liquip.WASM.VM;

namespace Liquip.Process.WASM;

public class WASMProcess: IProcess
{
    private readonly Host _host;

    public WASMProcess(string path)
    {
        _host = new Host();
        _host.LoadModule(new WASMProcessModule(this, _host, File.ReadAllBytes(path)));
    }
    public void SetWorkingDir(string wd)
    {
    }

    public void Environment(ProcessEnvironment env)
    {
        throw new NotImplementedException();
    }

    public int Main(string[] args)
    {
        throw new NotImplementedException();
    }
}
