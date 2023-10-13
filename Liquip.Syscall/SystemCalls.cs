namespace Liquip.Syscall;

/// <summary>
/// common syscalls
/// </summary>
public static class SystemCalls
{
    /// <summary>
    /// read from file
    /// </summary>
    public const uint SysRead  = 0;
    /// <summary>
    /// write to file
    /// </summary>
    public const uint SysWrite = 1;
    /// <summary>
    /// open file
    /// </summary>
    public const uint SysOpen  = 2;
    /// <summary>
    /// close and flush file buffer
    /// </summary>
    public const uint SysClose = 3;


    public const uint SysStat  = 4;
    public const uint SysFstat  = 5;
    public const uint SysLstat  = 6;

    public const uint SysSchedYield = 24;

    /// <summary>
    /// exit process
    /// </summary>
    public const uint SysExit = 60;

    /// <summary>
    /// kill a process
    /// </summary>
    public const uint SysKill = 62;
}
