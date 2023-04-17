// using Cosmos.Core;
// using Cosmos.HAL;
// using Cosmos.HAL.Network;
// using Cosmos.Zarlo.Logger.Interfaces;
// using Cosmos.Zarlo.Memory;
// using XSharp.Assembler.x86.x87;
//
// namespace Cosmos.Zarlo.Driver.VirtIO.Network;
//
// public class NetworkVirtio: NetworkDevice
// {
//     public const int RX_BUFFER_SIZE = 8192; 
//     public const int FRAME_SIZE = 1526;
//     private readonly PCIDevice _device;
//
//     public NetworkVirtio(PCIDevice device)
//     {
//         unsafe
//         {
//
//             _logger = Logger.Log.GetLogger(nameof(NetworkVirtio));
//             _device = device;
//             if (_device.VendorID != 0x1AF4)
//             {
//                 throw new NotSupportedException("the PCI device has the wrong vendor ID");
//             }
//             
//             ulong i;
//
//             VirtioDeviceInfo* devInfo = null;    
//             for (i=0;i<32;i++)
//             {
//                 if (_device.BaseAddressBar[i].BaseAddress==0)
//                 {
//                     devInfo = &_infoList[i];
//                     break;
//                 }
//             }
//             if (devInfo == null) throw new Exception("THE WORD IS ENDING");
//
//             devInfo->DeviceAddress = _device.BaseAddressBar[0].BaseAddress; // need to add logic to see get the right one
//             if (devInfo->DeviceAddress == 0xFFFFFFFF)
//             {
//                 throw new Exception("DEVICE NOT FOUND");
//             }
//
//             for (i=0;i<6;i++)
//             {
//                 PCIBaseAddressBar m = _device.BaseAddressBar[i];
//                 if (m==null) continue;
//                 if ((m.BaseAddress & 1) == 1)
//                 {
//                     devInfo->IoBase = (ushort)(m.BaseAddress & 0xFFFC);
//                 }
//                 else
//                 {
//                     devInfo->MemoryAddress = m.BaseAddress & 0xFFFFFFF0;
//                 }
//             }
//
//             devInfo->irq = (ushort)(devInfo->DeviceAddress & 0xFFFFFFF0);
//             INTs.SetIrqHandler((byte)devInfo->irq, IRQ_handler);
//             _device.EnableBusMaster(true);
//             _logger.Info($@"VirtIO netcard found. IRQ={devInfo->irq}, IO={devInfo->IoBase}");
//             
//         }
//         
//     }
//     
//     public override bool QueueBytes(byte[] buffer, int offset, int length)
//     {
//         throw new NotImplementedException();
//     }
//
//     public override bool ReceiveBytes(byte[] buffer, int offset, int max)
//     {
//         throw new NotImplementedException();
//     }
//
//     unsafe Queue<Pointer> _receivedPackets = new Queue<Pointer>();
//     
//     public override byte[] ReceivePacket()
//     {
//         var packet = _receivedPackets.Dequeue();
//         var output = new byte[packet.Size];
//         packet.CopyTo(output, 0, 0, packet.Size);
//         packet.Free();
//         return output;
//     }
//
//     public override int BytesAvailable()
//     {
//         if (_receivedPackets.Count == 0) return 0;
//         return (int)_receivedPackets.Peek().Size;
//     }
//
//     private VirtioDeviceInfo[] _infoList = new VirtioDeviceInfo[32];
//     private readonly ILogger _logger;
//
//     public override bool Enable()
//     {
//         unsafe
//         {
//
//             _ready = true;
//             return true;
//         }
//     }
//
//     void IRQ_handler(ref INTs.IRQContext context)
//     {
//         byte deviceIndex;
//         byte v;
//
//         // for each device that has data ready, ack the ISR
//         for (deviceIndex = 0; deviceIndex < 32; deviceIndex++)
//         {
//             unsafe
//             {
//                 fixed (VirtioDeviceInfo* dev = &_infoList[deviceIndex])
//                 {
//
//                     if (dev->IoBase==0) continue;
//
//                     //TODO: should not just blindly ack, should check if data is pending for that device.
//                     v = IOPort.Read8(dev->IoBase + 0x13);
//                     if ((v & 1) == 1)
//                     {
//                         Receive(dev);
//                     }
//                 }
//             }
//         }
//     }
//
//     private unsafe void Receive(VirtioDeviceInfo* dev)
//     {
//
//         fixed (VirtQueue* vq = &dev->Queues[0]) // RX queue
//         {
//             VirtIOManager.virtio_disable_interrupts(vq);
//
//             if (vq->LastUsedIndex == vq->Used->Index) return;
//         
//             ushort index = (ushort)(vq->LastUsedIndex % vq->QueueSize);
//             ushort buffer_index = vq->Used->Rings[index].Index;
//             uint* pointer = (uint*)(vq->QueueBuffer[buffer_index].Address + (ulong)sizeof(NetHeader));
//             _receivedPackets.Enqueue(new Pointer(pointer, (uint)vq->Used->Rings[index].Length-(uint)sizeof(NetHeader)));
//             
//             vq->LastUsedIndex++;
//
//             // we increase the available queue size but we dont need to put the buffer back in
//             // the ring because it is already there. The available->index is already "queue_size" ahead of us
//             // so increaseing by 1 will point to the ring entry of the current buffer since it will wrap around    
//             BufferInfo bi = new BufferInfo
//             {
//                 Size = FRAME_SIZE,
//                 Buffer = Pointer.Null,
//                 Flags = (byte)DescFlags.WriteOnly
//             };
//             VirtIOManager.virtio_send_buffer(dev, 0, bi);
//             
//             
//             VirtIOManager.virtio_enable_interrupts(vq);
//         }
//
//     }
//     public override bool IsSendBufferFull()
//     {
//         return false;
//     }
//
//     public override bool IsReceiveBufferFull()
//     {
//         return false;
//     }
//
//     public override CardType CardType => CardType.Ethernet;
//     
//     
//     public override MACAddress MACAddress { get; }
//
//
//     public override string Name => "VirtIONet";
//     private bool _ready = false;
//     public override bool Ready => _ready;
//     
//     
// }