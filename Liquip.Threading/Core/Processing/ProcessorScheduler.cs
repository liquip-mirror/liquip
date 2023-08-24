using System;
using System.Collections.Generic;
using Cosmos.Core;
using Cosmos.Core.Memory;
using IL2CPU.API.Attribs;
using Liquip.Core;
using Liquip.Threading.Core.Context;
using XSharp.Assembler;
using Zarlo.Cosmos.Logger;
using Zarlo.Cosmos.Logger.Interfaces;
using CCore = Cosmos.Core;
using HAL = Cosmos.HAL;

namespace Liquip.Threading.Core.Processing;


public static unsafe class ProcessorScheduler
{
    private static ILogger _logger = Log.GetLogger("Processor Scheduler");

    /// <summary>
    /// Setup the task Scheduler and starts process 0
    /// </summary>
    public static void Initialize()
    {
        _logger.Info("setting up the Processor Scheduler");
        var i = false;
        if (i)
        {
            ProcessorScheduler.SwitchTask();
            ProcessorScheduler.EntryPoint();
        }

        _logger.Info("setting up Boot context");
        ProcessContextManager.StartContext(
        "boot",
        null,
        ProcessContextType.PROCESS
            );
        _logger.Info("setting up Boot context DONE");

        _logger.Info("setting up PIT");
        int divisor = 1193182 / 25;
        IOPort.Write8(0x43, (0x06 | 0x30));
        IOPort.Write8(0x40, (byte)divisor);
        IOPort.Write8(0x40, (byte)(divisor >> 8));

        IOPort.Write8(0xA1, 0x00);
        IOPort.Write8(0xA1, 0x00);
        _logger.Info("setting up PIT DONE");
        _logger.Info("setting up the Processor Scheduler DONE");
    }

    [ForceInclude]
    public static void EntryPoint()
    {
        ProcessContextManager.CurrentContext.Entry?.Invoke();
        ProcessContextManager.CurrentContext.ParamEntry?.Invoke(
            ProcessContextManager.CurrentContext.Param
            );
        ProcessContextManager.CurrentContext.State = ThreadState.DEAD;
        while (true) {
        } // remove from thread pool later
    }

    public static int interruptCount;


    /// <summary>
    /// this is called by the Interrupt
    /// </summary>
    [ForceInclude]
    public static void SwitchTask()
    {

        CPU.DisableInterrupts();
        unchecked
        {
            interruptCount++;
        }

        // Console.WriteLine("SwitchTask {0}", interruptCount);
        var totalTasks = 0;
        for (var node = ProcessContextManager.ContextListHead; node != null; node = node.Next)
        {
            if (node.State == ThreadState.WAITING_SLEEP)
            {
                node.SleepUntil -= 1000 / 25;
                if (node.SleepUntil <= 0)
                {
                    node.State = ThreadState.ALIVE;
                }
            }

            totalTasks++;
        }

        // find the next none dead thread
        var nextCtx = ProcessContextManager.CurrentContext?.Next;

        if (nextCtx == null)
        {
            nextCtx = ProcessContextManager.ContextListHead;
        }

        while (nextCtx != null)
        {
            if (nextCtx.State == ThreadState.DEAD)
            {
                nextCtx = nextCtx.Next;

            }
            else
            {
                break;
            }
        }

        // save stack
        ProcessContextManager.CurrentContext.ESP = ZINTs.mStackContext;
        // update context
        ProcessContextManager.CurrentContext = nextCtx;
        // Console.WriteLine("context count: {1}, Id: {0}", ProcessContextManager.CurrentContext.Id, totalTasks);
        //load stack
        ZINTs.mStackContext = ProcessContextManager.CurrentContext.ESP;


        CPU.EnableInterrupts();
        CCore.Global.PIC.EoiMaster();
        CCore.Global.PIC.EoiSlave();
    }

    /// <summary>
    /// kills a process and all its sub process
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="sig"></param>
    public static void KillProcess(uint pid, uint sig)
    {
        uint limit = 100;
        DoKillProcess(pid, sig, ref limit);
    }

    private static void DoKillProcess(uint pid, uint sig, ref uint limit)
    {
        if(limit == 0) return;

        limit--;

        var processContext = ProcessContextManager.GetContext(pid);
        if(processContext.Equals(ProcessContext.NULL))
        {
            return;
        }

        if (processContext.Type is ProcessContextType.PROCESS or ProcessContextType.PROCESS_FORK)
        {
            for (var ctx = ProcessContextManager.ContextListHead; ctx != null; ctx = ctx.Next)
            {

                if (ctx.ParentId == pid)
                {
                    if (ctx.Type == ProcessContextType.THREAD)
                    {
                        ctx.State = ThreadState.DEAD;
                    }
                    else
                    {
                        KillProcess(ctx.Id, sig);
                    }
                }
            }
        }
    }

    /// <summary>
    /// removes dead Tasks from the task list
    /// </summary>
    public static void CleanUp()
    {
        ProcessContextManager.ContextListMutex.Lock();
        for (var ctx = ProcessContextManager.ContextListHead; ctx != null; ctx = ctx.Next)
        {
            // if (ctx.Value.State == ThreadState.DEAD)
            // {
            //     ProcessContextManager.ContextList.Remove(ctx);
            // }
        }
        ProcessContextManager.ContextListMutex.Unlock();
    }

}
