// ReSharper disable InconsistentNaming

using System;
using System.Threading;
using Cosmos.Core;
using Cosmos.Core.Memory;
using Cosmos.Zarlo;
using Cosmos.Zarlo.Driver.VirtIO;
using Cosmos.Zarlo.Threading;
using Cosmos.Zarlo.Threading.Core.Processing;

using Sys = Cosmos.System;

namespace Test;

public class Kernel : Sys.Kernel
{

    protected override void OnBoot()
    {
        base.OnBoot();
        
        ProcessorScheduler.Initialize();

    }

    public void runGC()
    { 
        while (true)
        {
            Cosmos.Zarlo.Threading.Thread.Sleep(1000);
            // Heap.Collect();
        }
    }

    protected override void BeforeRun()
    {
        Console.Clear();
        Console.WriteLine("Cosmos.Zarlo Test");

    }

    protected override void Run()
    {

        var devices = VirtIOManager.GetDevices();

        foreach (var item in devices)
        {
            Console.WriteLine(item.DebugInfo());
        }

        while (true)
        {
        }

    }


}

