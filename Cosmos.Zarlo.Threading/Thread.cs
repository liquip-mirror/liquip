namespace Cosmos.Zarlo.Threading;


public class Thread
{

    public static uint SpawnThread(ThreadStart aStart)
    {
        return Core.Processing.ProcessContext.StartContext(
            "", 
            aStart, 
            Core.Processing.ProcessContext.Context_Type.THREAD
        );
    }

    public static uint SpawnThread(ParameterizedThreadStart aStart, object param)
    {
        return Core.Processing.ProcessContext.StartContext(
            "", 
            aStart, 
            Core.Processing.ProcessContext.Context_Type.THREAD, 
            param
            );
    }


    public uint ThreadID;
    private Core.Processing.ProcessContext.Context Data;

    public Thread(uint threadID)
    {
        ThreadID = threadID;
        Data = Core.Processing.ProcessContext.GetContext(ThreadID);
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
        Data = Core.Processing.ProcessContext.GetContext(ThreadID);
        Data.state = Core.Processing.ProcessContext.Thread_State.PAUSED;
    }

    public void Start()
    {
        Data.state = Core.Processing.ProcessContext.Thread_State.ALIVE;
    }

    public void Stop()
    {
        Data.state = Core.Processing.ProcessContext.Thread_State.PAUSED;
    }

    public void Kill()
    {
        Data.state = Core.Processing.ProcessContext.Thread_State.DEAD;
    }

    public static void Sleep(int ms)
    {
        Core.Processing.ProcessContext.m_CurrentContext.arg = ms;
        Core.Processing.ProcessContext.m_CurrentContext.state = Core.Processing.ProcessContext.Thread_State.WAITING_SLEEP;
        while (Core.Processing.ProcessContext.m_CurrentContext.state == Core.Processing.ProcessContext.Thread_State.WAITING_SLEEP) 
        { 
            // swap task there
        }
    }
}