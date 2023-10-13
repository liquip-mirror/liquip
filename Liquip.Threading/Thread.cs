
using System;
using Cosmos.Core;
using Liquip.Threading.Core.Context;
using Liquip.Threading.Core.Processing;
using ThreadState = Liquip.Threading.Core.Context.ThreadState;

namespace Liquip.Threading;


/// <summary>
/// a high level abstraction for Threads
/// </summary>
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


    /// <summary>
    /// returns the current thread
    /// </summary>
    public static Thread Current => new Thread(ProcessContextManager.CurrentContext.Id);


    public readonly uint ThreadID;
    private ProcessContext Data => ProcessContextManager.GetContext(ThreadID);

    public Thread(uint threadID)
    {
        ThreadID = threadID;
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
        Data.State = ThreadState.PAUSED;
    }

    public void Start()
    {
        Data.State = ThreadState.ALIVE;
    }

    public void Stop()
    {
        Data.State = ThreadState.PAUSED;
    }

    public void Kill()
    {
        Data.State = ThreadState.DEAD;
    }

    public static void Yield()
    {
    }

    public static void Sleep(uint ms)
    {
        var context = ProcessContextManager.CurrentContext;
        context.SleepUntil = ms;
        context.State = ThreadState.WAITING_SLEEP;
        while (context.State == ThreadState.WAITING_SLEEP)
        {
            Yield();
        }
    }
}
