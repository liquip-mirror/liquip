using System;
using System.IO;
using Liquip.Common;
using Liquip.WASM.Interfaces;


namespace Liquip.Process.WASM;

public class WASMProcess: IProcess
{

    public WASMProcess(string path)
    {

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
