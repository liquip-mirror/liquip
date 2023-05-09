using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.HAL.Network;
using Cosmos.Zarlo.Logger.Interfaces;
using Cosmos.Zarlo.Memory;
using XSharp.Assembler.x86.x87;

namespace Cosmos.Zarlo.Driver.VirtIO.Network;

public class NetworkVirtio: BaseVirtIODevice
{
    public const int RX_BUFFER_SIZE = 8192; 
    public const int FRAME_SIZE = 1526;
    private readonly PCIDevice _device;

    unsafe Queue<Pointer> _receivedPackets = new Queue<Pointer>();

    void Check()
    { 
        if(DeviceID != (ushort)(DeviceTypeVirtIO.NetworkCard)) throw new NotSupportedException(string.Format("wrong DeviceID {0}", DeviceID));
    }

    public NetworkVirtio(uint bus, uint slot, uint function) : base(bus, slot, function)
    {
        Check();
    }

    public NetworkVirtio(PCIDevice device) : base(device)
    {
        Check();
    }

    public bool QueueBytes(byte[] buffer, int offset, int length)
    {
        throw new NotImplementedException();
    }

    public bool ReceiveBytes(byte[] buffer, int offset, int max)
    {
        throw new NotImplementedException();
    }

    public byte[] ReceivePacket()
    {
        var packet = _receivedPackets.Dequeue();
        var output = new byte[packet.Size];
        packet.CopyTo(output, 0, 0, packet.Size);
        packet.Free();
        return output;
    }

    public int BytesAvailable()
    {
        if (_receivedPackets.Count == 0) return 0;
        return (int)_receivedPackets.Peek().Size;
    }

    void IRQ_handler(ref INTs.IRQContext context)
    {

    }

    
    
    public MACAddress MACAddress { get; }
    
    
}