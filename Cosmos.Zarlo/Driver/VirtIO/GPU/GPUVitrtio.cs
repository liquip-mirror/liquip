using Cosmos.HAL;
using Cosmos.Zarlo.Memory;

namespace Cosmos.Zarlo.Driver.VirtIO.GPU;

public class GPUVirtIODevice: BaseVirtIODevice
{
    public Pointer frameBuffer;
    private readonly PCIDevice _device;

    public const int ControlQueue = 0;
    public const int CursorQueue = 1;

    void Check()
    { 
        if(DeviceID != (ushort)(DeviceTypeVirtIO.GPU_device)) throw new NotSupportedException(string.Format("wrong DeviceID {0}", DeviceID));
    }

    public GPUVirtIODevice(uint bus, uint slot, uint function) : base(bus, slot, function)
    {
    }

    public GPUVirtIODevice(PCIDevice device) : base(device)
    {
    }


    public override void Initialization()
    {
        // _logger.Info("Initialization");
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