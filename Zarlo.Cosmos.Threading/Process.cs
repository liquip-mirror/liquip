using Zarlo.Cosmos.Threading.Core.Context;
using Zarlo.Cosmos.Threading.Core.Processing;

namespace Zarlo.Cosmos.Threading;


public class Process 
{

    public static uint SpawnProcess(ThreadStart aStart, string name = "nameless")
    {
        return ProcessContextManager.StartContext(
            name, 
            aStart, 
            ProcessContextType.PROCESS
        );
    }

    public static uint SpawnProcess(ParameterizedThreadStart aStart, object param, string name = "nameless")
    {
        return ProcessContextManager.StartContext(
            name, 
            aStart, 
            ProcessContextType.PROCESS, 
            param
            );
    }

    public Thread MainTread;

    public Process(ThreadStart start)
    {
        MainTread = new Thread(SpawnProcess(start));
        ProcessFinalSetup();
    }

    public Process(ParameterizedThreadStart start, object param)
    {
        MainTread = new Thread(SpawnProcess(start, param));
        ProcessFinalSetup();
    }

    private void ProcessFinalSetup()
    {
        MainTread.Stop();
    }

    public void Start()
    {
        MainTread.Start();
    }

    public void Kill(uint sig)
    {
        ProcessorScheduler.KillProcess(MainTread.ThreadID, sig);
        MainTread.Kill();
    }

}