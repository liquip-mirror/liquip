namespace Liquip.Syscall;

public enum SystemCalls: uint
{
    sys_read  = 0,
    sys_write = 1,
    sys_open  = 2,
    sys_close = 3,
    sys_stat  = 4,
    sys_fstat  = 5,
    sys_lstat  = 6,

    sys_sched_yield = 24,

    sys_exit = 60,
    sys_kill = 62
}
