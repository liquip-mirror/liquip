using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.Zarlo.Memory;

namespace Cosmos.Zarlo.Driver.VirtIO.GPU;

public class GPUVirtIOHead: IDisposable
{
    public readonly GPUVirtIODevice Driver;

    internal GPUVirtIOHead(GPUVirtIODevice driver)
    {
        Driver = driver;
    }

    public bool IsEnabled { get; private set; }

    public Pointer FrameBuffer;

    public void Clear()
    {
        FrameBuffer.Fill(0x00);
    }

    public void Dispose()
    {
        FrameBuffer.Dispose();
    }
}

public class GPUVirtIODevice: BaseVirtIODevice
{
    public GPUVirtIOHead[] Heads;
    private readonly PCIDevice _device;

    public const int ControlQueue = 0;
    public const int CursorQueue = 1;

    void Check()
    { 
        if(DeviceID != (ushort)(DeviceTypeVirtIO.GPU_device)) throw new NotSupportedException(string.Format("wrong DeviceID {0}", DeviceID));
    }


    public GPUVirtIODevice(uint bus, uint slot, uint function) : base(bus, slot, function)
    {
        Check();
    }

    public GPUVirtIODevice(PCIDevice device) : base(device)
    {
        Check();
    }

    public void SendCommand(uint index, ref Pointer command)
    { 

    }

    public void SendCursorUpdate(uint x, uint y)
    { 

        

    }

    public override void Initialization()
    {
        // _logger.Info("Initialization");
        Console.WriteLine(DebugInfo());
        // ACKNOWLEDGE();
        // GPUDeviceFeatureFlag flag = GPUDeviceFeatureFlag.VIRGL | GPUDeviceFeatureFlag.EDID;
        // SetDeviceFeaturesFlag((byte)flag);
        // IS_FEATURES_OK();
        // FEATURES_OK();
        // DRIVER();
        // DRIVER_OK();
        // _logger.Info("Initialization Done");
    }

}