namespace Liquip.Threading.Core.Context;


public enum ThreadState
{
    ALIVE = 0,
    DEAD = 1,
    WAITING_SLEEP = 2,
    PAUSED = 3,
    WAITING_IO = 4,
}

public enum ProcessContextType
{
    THREAD = 0,
    PROCESS = 1,
    PROCESS_FORK = 2,
    SYSCALL = 3
}



public class ProcessContext
{
    public ProcessContext next;
    public ProcessContextType type;
    public uint tid;
    public string name;
    public uint esp;
    public uint stacktop;
    public ThreadStart entry;
    public ParameterizedThreadStart paramentry;
    public ThreadState state;
    public object param;
    public int arg;
    public uint priority;
    public uint age;
    public uint parent;

}
