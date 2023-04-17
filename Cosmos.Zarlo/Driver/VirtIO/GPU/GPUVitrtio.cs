using Cosmos.HAL;
using Cosmos.Zarlo.Memory;

namespace Cosmos.Zarlo.Driver.VirtIO.GPU;

public class GPUVirtIO
{
    public Pointer frameBuffer;
    private readonly PCIDevice _device;

    public const int ControlQueue = 0;
    public const int CursorQueue = 1;

    public GPUVirtIO(PCIDevice device)
    {
        _device = device;
    }

    public void SendQueue(int queue)
    {
        
    }

}