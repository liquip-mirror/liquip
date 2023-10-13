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
    SYSCALL = 3,
    NULL = -1
}


/// <summary>
/// stores the Process context
/// </summary>
public class ProcessContext
{

    public static ProcessContext NULL = new ProcessContext()
    {
        Id = uint.MaxValue,
        ESP = 0,
        Name = "NULL",
        OwnerGid = 1,
        OwnerUid = 1,
        ParentId = 0,
    };

    /// <summary>
    /// is the type of task this context if for
    /// </summary>
    public ProcessContextType Type { get; set; }

    /// <summary>
    /// get the threadTd
    /// </summary>
    public uint Id { get; set; }
    /// <summary>
    /// a name of the thread may be blank
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    ///
    /// </summary>
    public uint ESP { get; set; }
    /// <summary>
    ///
    /// </summary>
    public uint Stacktop { get; set; }

    /// <summary>
    ///
    /// </summary>
    public ThreadStart Entry { get; set; }
    /// <summary>
    ///
    /// </summary>
    public ParameterizedThreadStart ParamEntry { get; set; }

    /// <summary>
    /// used to handle events
    /// </summary>
    public ThreadHandle HadleEntry { get; set; }

    /// <summary>
    ///
    /// </summary>
    public ThreadState State { get; set; }
    /// <summary>
    ///
    /// </summary>
    public object Param { get; set; }

    public uint Priority { get; set; }

    public uint OwnerUid { get; set; }
    public uint OwnerGid { get; set; }

    /// <summary>
    /// context parent id
    /// </summary>
    public uint ParentId { get; set; }

    public uint SleepUntil { get; set; } = 0;

    public ProcessContext Next { get; set; }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj?.GetType() == typeof(ProcessContext))
        {
            if ((obj as ProcessContext)?.Id == Id)
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return (int)Id;
    }
}
