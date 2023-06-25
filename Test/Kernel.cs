// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using Cosmos.Core;
using Cosmos.Core.Memory;
using Zarlo.Cosmos;
using Zarlo.Cosmos.Driver.VirtIO;
using Zarlo.Cosmos.Threading;
using Zarlo.Cosmos.Threading.Core;
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

    public void runMain()
    {
        while (true)
        {
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Main");
            List<uint> tids = new List<uint>();
            var c = ProcessContextManager.m_CurrentContext;
            while (c != null)
            {
                tids.Add(c.tid);
                c = c.next;
            }

            Console.WriteLine("");
            Console.SetCursorPosition(0, 6);
            Console.WriteLine("TIDs:" + string.Join(',', tids));
            Console.WriteLine("Cycle {0}", ProcessorScheduler.interruptCount);

            Console.WriteLine("Done Main");

        }
    }

    public void runProcess()
    {
        while (true)
        {
            Console.SetCursorPosition(0, 10 + ((int)Thread.Current.ThreadID * 2));
            Console.WriteLine("Test Process PID {0}", Thread.Current.ThreadID);
            Console.WriteLine("Cycle {0}", ProcessorScheduler.interruptCount);
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(1000);
        }
    }

    Thread mainThread;
    Process process1;
    Process process2;
    Process process3;
    Process process4;

    protected override void BeforeRun()
    {
        Console.Clear();
        Console.WriteLine("Zarlo.Cosmos Test");

        mainThread = new Thread(runMain);
        mainThread.Start();

        process1 = new Process(runProcess);
        process1.MainTread.Start();

        process2 = new Process(runProcess);
        process2.MainTread.Start();

        process3 = new Process(runProcess);
        process3.MainTread.Start();

        process4 = new Process(runProcess);
        process4.MainTread.Start();

    }

    protected override void Run()
    {

    }


}
