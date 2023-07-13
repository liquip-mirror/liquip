using System;
using Cosmos.HAL;
using Liquip.Driver.VirtIO.GPU.Struct;
using Liquip.Memory;

namespace Liquip.Driver.VirtIO.GPU;

public class GPUVirtIOHead : IDisposable
{
    public readonly GPUVirtIODevice Driver;

    public Pointer FrameBuffer;

    internal GPUVirtIOHead(GPUVirtIODevice driver)
    {
        Driver = driver;
    }

    public bool IsEnabled { get; }

    public void Dispose()
    {
        FrameBuffer.Dispose();
    }

    public void Clear()
    {
        FrameBuffer.Fill(0x00);
    }
}

public class GPUVirtIODevice : BaseVirtIODevice
{
    public const int ControlQueue = 0;
    public const int CursorQueue = 1;
    private readonly PCIDevice _device;

    private readonly int _resourceId = 0;
    public GPUVirtIOHead[] Heads;


    public GPUVirtIODevice(uint bus, uint slot, uint function) : base(bus, slot, function)
    {
        Check();
    }

    public GPUVirtIODevice(PCIDevice device) : base(device)
    {
        Check();
    }

    private void Check()
    {
        if (DeviceID != (ushort)DeviceTypeVirtIO.GPU_device)
        {
            throw new NotSupportedException(string.Format("wrong DeviceID {0}", DeviceID));
        }
    }


    public void SendCursorMove(uint scanoutId, uint x, uint y)
    {
        var command = new GpuUpdateCursor
        {
            Header = new GPUCtrlHdr { Type = GpuCmd.MOVE_CURSOR },
            Pos = new GpuCursorPos { ScanoutId = scanoutId, X = x, Y = y }
        };
        SendCommand(CursorQueue, ref command, DescFlags.WriteOnly);
    }

    public Resource2d Create2dResource(
        GpuFormats format,
        int width,
        int height
    )
    {
        var id = _resourceId;
        var command = new GpuResourceCreate2d
        {
            Header = new GPUCtrlHdr { Type = GpuCmd.RESOURCE_CREATE_2D },
            ResourceId = id,
            Format = format,
            Width = width,
            Height = height
        };
        SendCommand(ControlQueue, ref command, DescFlags.WriteOnly);

        return new Resource2d { Id = id, Format = format, Width = width, Height = height };
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
