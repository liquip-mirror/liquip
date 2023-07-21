// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using Liquip;
using Liquip.Threading;
using Liquip.Threading.Core.Processing;
using Liquip.WASM;
using Sys = Cosmos.System;

namespace Test;

public class Kernel : Sys.Kernel
{
    private Thread clearThread;

    private readonly ThreadStatic<int> Cycle = new();

    private Thread mainThread;
    private Process process1;
    private Process process2;
    private Process process3;
    private Process process4;

    protected override void OnBoot()
    {
        base.OnBoot();

        ProcessorScheduler.Initialize();
    }

    public void runMain()
    {
        while (true)
        {
            Thread.Sleep(1000);
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
            Thread.Sleep(1000);
        }
    }

    public void runClear()
    {
        while (true)
        {
            Console.Clear();
            Thread.Sleep(10000);
        }
    }

    protected override void BeforeRun()
    {
        Console.Clear();
        Console.WriteLine("Liquip Test");

        mainThread = new Thread(runMain);
        mainThread.Start();

        clearThread = new Thread(runClear);
        clearThread.Start();

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
