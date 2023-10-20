using System;

namespace Liquip.Threading;

/// <summary>
/// a simple Mutext
/// </summary>
public class Mutex
{
    private int gate;

    /// <summary>
    /// tryed o get the lock
    /// </summary>
    /// <param name="ms">time out</param>
    /// <exception cref="TimeoutException"></exception>
    public void Lock(uint ms)
    {
        while (gate != 0)
        {
            Thread.Sleep(10);
            ms -= 10;
            if (ms <= 0)
            {
                throw new TimeoutException("failed to get log in time ");
            }
        }

        gate = 1;
    }

    /// <summary>
    /// tries to get the lock with a 4294967295ms time out ~49 days
    /// </summary>
    /// <exception cref="TimeoutException"></exception>
    public void Lock()
    {
        Lock(uint.MaxValue);
    }

    public void Unlock()
    {
        gate = 0;
    }
}
