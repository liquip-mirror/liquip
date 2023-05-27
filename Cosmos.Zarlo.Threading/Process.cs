using Cosmos.Zarlo.Threading.Core.Processing;

namespace Cosmos.Zarlo.Threading;


public class Process 
{

    public static uint SpawnProcess(ThreadStart aStart, string name = "nameless")
    {
        return Core.Processing.ProcessContext.StartContext(
            name, 
            aStart, 
            Core.Processing.ProcessContext.Context_Type.PROCESS
        );
    }

    public static uint SpawnProcess(ParameterizedThreadStart aStart, object param, string name = "nameless")
    {
        return Core.Processing.ProcessContext.StartContext(
            name, 
            aStart, 
            Core.Processing.ProcessContext.Context_Type.PROCESS, 
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

    public void Kill(uint sig)
    {
        ProcessorScheduler.KillProcess(MainTread.ThreadID, sig);
        MainTread.Kill();
    }

}