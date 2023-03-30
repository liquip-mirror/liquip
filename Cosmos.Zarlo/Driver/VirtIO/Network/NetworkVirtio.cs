using Cosmos.HAL;
using Cosmos.HAL.Network;

namespace Cosmos.Zarlo.Driver.VirtIO.Network;

public class NetworkVirtio: NetworkDevice
{
    private readonly PCIDevice _device;

    public NetworkVirtio(PCIDevice device)
    {
        _device = device;
        if (_device.VendorID != 0x1AF4)
        {
            throw new NotSupportedException("the PCI device has the wrong vendor ID");
        }
        _device.EnableDevice();

    }

    public override bool QueueBytes(byte[] buffer, int offset, int length)
    {
        throw new NotImplementedException();
    }

    public override bool ReceiveBytes(byte[] buffer, int offset, int max)
    {
        throw new NotImplementedException();
    }

    public override byte[] ReceivePacket()
    {
        throw new NotImplementedException();
    }

    public override int BytesAvailable()
    {
        throw new NotImplementedException();
    }

    public override bool Enable()
    {
        throw new NotImplementedException();
    }

    public override bool IsSendBufferFull()
    {
        throw new NotImplementedException();
    }

    public override bool IsReceiveBufferFull()
    {
        throw new NotImplementedException();
    }

    public override CardType CardType { get; }
    public override MACAddress MACAddress { get; }
    public override string Name { get; }
    public override bool Ready { get; }
}