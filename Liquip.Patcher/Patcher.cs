

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Liquip.Patcher;

public class Patcher
{
    public HashSet<Assembly> PatchFiles = new HashSet<Assembly>();
    public Assembly _assembly;
    public Patcher(Assembly assembly)
    {
        _assembly = assembly;
    }

    public void AddPatch(Assembly assembly)
    {
        PatchFiles.Add(assembly);
    }

    public Span<byte> Build()
    {
        AssemblyBuilder builder = AssemblyBuilder.DefineDynamicAssembly("asd.dll");
        return null;
    }
}
