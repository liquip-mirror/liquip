namespace Liquip.Threading.Core.Context;


/// <summary>
/// Task State
/// </summary>
public enum ThreadState
{
    /// <summary>
    /// will be used when looking for a new task
    /// </summary>
    ALIVE = 0,

    /// <summary>
    /// will be skipped then looking for a new task
    /// </summary>
    DEAD = 1,

    /// <summary>
    /// sleep counter will be updated then it will be skipped then looking for a new task
    /// </summary>
    WAITING_SLEEP = 2,

    /// <summary>
    /// will be skipped then looking for a new task
    /// </summary>
    PAUSED = 3,

    /// <summary>
    /// will be skipped then looking for a new task
    /// </summary>
    WAITING_IO = 4,
}

public enum ProcessContextType
{
    THREAD = 0,
    PROCESS = 1,
    PROCESS_FORK = 2,
    SYSCALL = 3
}


/// <summary>
/// stores the Process context
/// </summary>
public class ProcessContext
{
    /// <summary>
    /// gets the next item in the list
    /// </summary>
    public ProcessContext next;
    /// <summary>
    /// is the type of task this context if for
    /// </summary>
    public ProcessContextType type;
    /// <summary>
    /// get the threadTd
    /// </summary>
    public uint tid;
    /// <summary>
    /// a name of the thread may be blank
    /// </summary>
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
