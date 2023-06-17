
using Zarlo.Cosmos.Threading;

namespace Zarlo.Cosmos.Threading;

public class Mutex
{
    public int gate;

    public void Lock()
    {
        while (gate != 0) { }
        gate = 1;

    }

    public void Unlock()
    {
        gate = 0;
    }
}