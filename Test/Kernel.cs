// ReSharper disable InconsistentNaming

using System;
using System.Threading;
using Cosmos.Core;
using Cosmos.Core.Memory;
using Zarlo.Cosmos;
using Zarlo.Cosmos.Driver.VirtIO;
using Zarlo.Cosmos.Threading;
using Zarlo.Cosmos.Threading.Core.Processing;

using Sys = Cosmos.System;

namespace Test;

public class Kernel : Sys.Kernel
{

    protected override void OnBoot()
    {
        base.OnBoot();
        
        ProcessorScheduler.Initialize();
        var a = false;
        if (a)
        { 
            int eax = 0;
            int ebx = 0; 
            int ecx = 0; 
            int edx = 0;
            CPUID.Raw(1, 1,
                ref eax,
                ref ebx,
                ref ecx,
                ref edx
            );
        }

    }

    public void runGC()
    { 
        while (true)
        {
            Zarlo.Cosmos.Threading.Thread.Sleep(1000);
            // Heap.Collect();
        }
    }

    protected override void BeforeRun()
    {
        Console.Clear();
        Console.WriteLine("Zarlo.Cosmos Test");

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

