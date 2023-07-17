using System;
using Cosmos.Core;
using Cosmos.Core.Memory;
using IL2CPU.API.Attribs;
using Liquip.Core;
using Liquip.Threading.Core.Context;
using CCore = Cosmos.Core;
using HAL = Cosmos.HAL;

namespace Liquip.Threading.Core.Processing;


public static unsafe class ProcessorScheduler
{
    public static void Initialize()
    {


        var i = false;
        if (i)
        {
            ProcessorScheduler.SwitchTask();
            ProcessorScheduler.EntryPoint();
        }

        var context = new ProcessContext();
        context.type = ProcessContextType.PROCESS;
        context.tid = ProcessContextManager.m_NextCID++;
        context.name = "Boot";
        context.esp = 0;
        context.stacktop = 0;
        context.state = ThreadState.ALIVE;
        context.arg = 0;
        context.priority = 0;
        context.age = 0;
        context.parent = 0;
        GCImplementation.IncRootCount((ushort*)GCImplementation.GetPointer(context));
        ProcessContextManager.m_ContextList = context;
        ProcessContextManager.m_CurrentContext = context;


        int divisor = 1193182 / 25;
        IOPort.Write8(0x43, (0x06 | 0x30));
        IOPort.Write8(0x40, (byte)divisor);
        IOPort.Write8(0x40, (byte)(divisor >> 8));

        IOPort.Write8(0xA1, 0x00);
        IOPort.Write8(0xA1, 0x00);
    }

    [ForceInclude]
    public static void EntryPoint()
    {
        ProcessContextManager.m_CurrentContext.entry?.Invoke();
        ProcessContextManager.m_CurrentContext.paramentry?.Invoke(ProcessContextManager.m_CurrentContext.param);
        ProcessContextManager.m_CurrentContext.state = ThreadState.DEAD;
        while (true) {
        } // remove from thread pool later
    }

    public static int interruptCount;

    public static void DoSwitchTask()
    {
        // Console.WriteLine("SwitchTask {0}", interruptCount);
        if (ProcessContextManager.m_CurrentContext != null)
        {
            ProcessContext ctx = ProcessContextManager.m_ContextList;
            ProcessContext last = ctx;
            while (ctx != null)
            {
                if (ctx.state == ThreadState.DEAD)
                {
                    last.next = ctx.next;
                    break;
                }
                last = ctx;
                ctx = ctx.next;
            }
            ctx = ProcessContextManager.m_ContextList;
            while (ctx != null)
            {
                if (ctx.state == ThreadState.WAITING_SLEEP)
                {
                    ctx.arg -= 1000 / 25;
                    if (ctx.arg <= 0)
                    {
                        ctx.state = ThreadState.ALIVE;
                    }
                }
                ctx.age++;
                ctx = ctx.next;
            }
            ProcessContextManager.m_CurrentContext.esp = ZINTs.mStackContext;
            tryagain:;
            if (ProcessContextManager.m_CurrentContext.next != null)
            {
                ProcessContextManager.m_CurrentContext = ProcessContextManager.m_CurrentContext.next;
            }
            else
            {
                ProcessContextManager.m_CurrentContext = ProcessContextManager.m_ContextList;
            }
            if (ProcessContextManager.m_CurrentContext.state != ThreadState.ALIVE)
            {
                goto tryagain;
            }
            ProcessContextManager.m_CurrentContext.age = ProcessContextManager.m_CurrentContext.priority;
            ZINTs.mStackContext = ProcessContextManager.m_CurrentContext.esp;
        }
    }

    [ForceInclude]
    public static void SwitchTask()
    {

        // if(!HAL.Global.InterruptsEnabled) return;

        CPU.DisableInterrupts();
        unchecked
        {
            interruptCount++;
        }

        DoSwitchTask();
        CPU.EnableInterrupts();
        CCore.Global.PIC.EoiMaster();
        CCore.Global.PIC.EoiSlave();
    }

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

        if (processContext.type is ProcessContextType.PROCESS or ProcessContextType.PROCESS_FORK)
        {
            ProcessContext ctx = ProcessContextManager.m_ContextList;
            while (ctx.next != null)
            {
                if (ctx.parent == pid)
                {
                    if (ctx.type == ProcessContextType.THREAD)
                    {
                        ctx.state = ThreadState.DEAD;
                    }
                    else
                    {
                        KillProcess(ctx.tid, sig);
                    }
                }
                ctx = ctx.next;
            }
            if (ctx.tid == pid)
            {
                if (ctx.type == ProcessContextType.THREAD)
                {
                    ctx.state = ThreadState.DEAD;
                }
                else
                {
                    KillProcess(ctx.tid, sig);
                }
            }
        }
    }

    public static void CleanUp()
    {
        ProcessContextManager.ContextListMutex.Lock();
        ProcessContext current = ProcessContextManager.m_ContextList;
        ProcessContext last = null;
        while (current != null)
        {
            if (current.state != ThreadState.DEAD)
            {
                last = current;
                current = current.next;
            }
            else
            {
                var next = current.next;
                if (last == null)
                {
                    ProcessContextManager.m_ContextList = next;
                }
                else
                {
                    last.next = next;
                }
                GCImplementation.Free(current);
                current = next;
            }


        }
        ProcessContextManager.ContextListMutex.Unlock();
    }

}
