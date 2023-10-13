// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using Liquip.Threading;
using Liquip.Threading.Core.Processing;
using Sys = Cosmos.System;
using Thread = Liquip.Threading.Thread;

namespace Test;

public class Kernel : Sys.Kernel
{
    private Thread clearThread;


    private readonly ThreadStatic<int> Cycle = new();

    private Thread mainThread;
    private Process process1;
    private Process process2;

    protected override void OnBoot()
    {
        base.OnBoot();
        ProcessorScheduler.Initialize();
    }

    public void runMain()
    {

        while (true)
        {
            // Thread.Sleep(1000);
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Main");
            var tids = new List<uint>();
            Cycle.Value = ProcessorScheduler.interruptCount;

            Console.WriteLine("");
            Console.SetCursorPosition(0, 6);
            Console.WriteLine("TIDs:" + string.Join(',', tids));
            Console.WriteLine("Cycle {0}", Cycle.Value);
            Console.WriteLine("Up time {0}", ProcessorScheduler.interruptCount / 50);

            Console.WriteLine("Done Main");
        }
    }

    public void runProcess()
    {
        while (true)
        {
            Cycle.Value = ProcessorScheduler.interruptCount;
            Console.SetCursorPosition(0, 10 + (int)Thread.Current.ThreadID * 2);
            Console.WriteLine("Test Process PID {0}", Thread.Current.ThreadID);
            Console.WriteLine("Cycle {0}", Cycle.Value);
            Console.SetCursorPosition(0, 0);
            // Thread.Sleep(1000);
        }
    }

    protected override void BeforeRun()
    {
        // Console.Clear();
        Console.WriteLine("Liquip Test");

        mainThread = new Thread(runMain);
        mainThread.Start();

        process1 = new Process(runProcess);
        process1.Start();

        process2 = new Process(runProcess);
        process2.Start();

    }

    protected override void Run()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Cycle {0}", Cycle.Value);
    }
}
