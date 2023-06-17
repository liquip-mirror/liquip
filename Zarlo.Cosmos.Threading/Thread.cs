
using System;
using Cosmos.Core;
using Zarlo.Cosmos.Threading.Core.Context;
using Zarlo.Cosmos.Threading.Core.Processing;

namespace Zarlo.Cosmos.Threading;


public class Thread
{

    public static uint SpawnThread(ThreadStart aStart)
    {
        return ProcessContextManager.StartContext(
            "", 
            aStart, 
            ProcessContextType.THREAD
        );
    }

    public static uint SpawnThread(ParameterizedThreadStart aStart, object param)
    {
        return ProcessContextManager.StartContext(
            "", 
            aStart, 
            ProcessContextType.THREAD, 
            param
            );
    }


    public static Thread Current => new Thread(ProcessContextManager.m_CurrentContext.tid);


    public readonly uint ThreadID;
    private ProcessContext Data;

    public Thread(uint threadID)
    {
        ThreadID = threadID;
        Data = ProcessContextManager.GetContext(ThreadID);
    }

    public Thread(ThreadStart start)
    {
        ThreadID = SpawnThread(start);
        ThreadFinalSetup();
    }

    public Thread(ParameterizedThreadStart start, object param)
    {
        ThreadID = SpawnThread(start, param);
        ThreadFinalSetup();
    }

    private void ThreadFinalSetup()
    {
        Data = ProcessContextManager.GetContext(ThreadID);
        Data.state = ThreadState.PAUSED;
    }

    public void Start()
    {
        Data.state = ThreadState.ALIVE;
    }

    public void Stop()
    {
        Data.state = ThreadState.PAUSED;
    }

    public void Kill()
    {
        Data.state = ThreadState.DEAD;
    }

    public static void Yield()
    {
        Console.WriteLine("yielding");
        ProcessorScheduler.SwitchTask();
    }

    public static void Sleep(int ms)
    {
        if(ProcessContextManager.m_CurrentContext == null) return;
        ProcessContextManager.m_CurrentContext.arg = ms;
        ProcessContextManager.m_CurrentContext.state = ThreadState.WAITING_SLEEP;
        while (ProcessContextManager.m_CurrentContext.state == ThreadState.WAITING_SLEEP) 
        {
            // Yield();
            // swap task there
        }
    }
}