using System;

namespace Zarlo.Cosmos.Threading;

public class Mutex
{
    private int gate;

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
            Thread.Yield();
        }

        gate = 1;
    }

    public void Lock()
    {
        Lock(uint.MaxValue);
    }

    public void Unlock()
    {
        gate = 0;
    }
}
