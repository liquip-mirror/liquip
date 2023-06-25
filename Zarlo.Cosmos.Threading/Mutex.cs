
using System;
using Microsoft.VisualBasic;

namespace Zarlo.Cosmos.Threading;

public class Mutex
{
    int gate;

    public void Lock(uint ms)
    {

        while (gate != 0) { 
            Thread.Sleep(10);
            ms -= 10;
            if (ms < 0)
            {
                throw new TimeoutException("faild to get log in time ");
            }
        }
        gate = 1;
    }

    public void Lock()
    {
        while (gate != 0) {
            Thread.Sleep(10);
        }
        gate = 1;
    }

    public void Unlock()
    {
        gate = 0;
    }
}