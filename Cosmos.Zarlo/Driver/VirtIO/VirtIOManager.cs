// using System.Runtime.InteropServices;
// using Cosmos.Core;
// using Cosmos.Core.Memory;
// using Cosmos.HAL;
// using Cosmos.Zarlo.Memory;
//
// namespace Cosmos.Zarlo.Driver.VirtIO;
//
// public unsafe static class VirtIOManager
// {
//     private static PCIDevice[]? _devices = null;
//     public static PCIDevice[] GetDevices()
//     {
//         if (_devices != null)
//         {
//             return _devices;
//         }
//
//         var output = new List<PCIDevice>();
//         foreach (PCIDevice device in PCI.Devices)
//         {
//             if (device.VendorID == 0x1AF4)
//                 output.Add(device);
//         }
//
//         _devices = output.ToArray();
//         return _devices;
//     }
//
//
//     public static bool virtio_init(VirtioDeviceInfo* dev, Action<uint> flags)
//     {
//         
//         //Virtual I/O Device (VIRTIO) Version 1.0, Spec 4, section 3.1.1:  Device Initialization
//         DeviceStatusFlag c = DeviceStatusFlag.ACKNOWLEDGE;
//         IOPort.Write8(dev->IoBase+0x12, (byte)c);
//         
//         c |= DeviceStatusFlag.DRIVER;
//         IOPort.Write8(dev->IoBase+0x12, (byte)c);
//
//         var features = IOPort.Read32(dev->IoBase + 0x00);
//         flags(features);
//         IOPort.Write32(dev->IoBase+0x04, features);
//         
//         c |= DeviceStatusFlag.FEATURES_OK;
//         IOPort.Write8(dev->IoBase+0x12, (byte)c);
//         var check = IOPort.Read8(dev->IoBase + 0x12)& (byte)DeviceStatusFlag.FEATURES_OK;
//         
//         if (check == 0)
//         {
//             //TODO: should set to driver_failed
//             Console.WriteLine("[VirtIO] Feature set not accepted\r\n");
//             return false;
//         }
//
//         // Setup virt queues
//         for (ushort i = 0; i < 16; i++) virtio_queue_setup(dev,i);
//
//         c |= DeviceStatusFlag.DRIVER_OK;
//         IOPort.Write8(dev->IoBase+0x12, (byte)c);
//         
//         return true;
//     }
//
//     public unsafe static bool virtio_queue_setup(VirtioDeviceInfo* dev, ushort index)
//     {
//         ushort c;
//         ushort queueSize; 
//         uint i;
//
//         fixed (VirtQueue* vq = &(dev->Queues[index]))
//         {
//
//             // memclear64(vq, sizeof(virt_queue));
//
//             // get queue size
//             IOPort.Write16(dev->IoBase+0x0E, index);
//             queueSize = IOPort.Read16(dev->IoBase + 0x0C);
//             vq->QueueSize = queueSize;
//             if (queueSize == 0) return false;
//
//             // create virtqueue memory
//             uint sizeofBuffers = (uint)(sizeof(QueueBuffer) * queueSize);
//             uint sizeofQueueAvailable = (uint)((2 * sizeof(ushort)) + (queueSize * sizeof(ushort)));
//             uint sizeofQueueUsed = (uint)((2 * sizeof(ushort)) + (queueSize * sizeof(VirtioUsedItem)));
//             uint size = sizeofBuffers + sizeofQueueAvailable + sizeofQueueUsed;
//             byte* buf = Heap.Alloc(size);
//             MemoryOperations.Fill(buf, 0x00, (int)size);
//
//             vq->BaseAddress = (ulong)buf;
//             vq->Available = (VirtioAvailable*)&buf[sizeofBuffers];
//             vq->Used = (VirtioUsed*)&buf[((sizeofBuffers + sizeofQueueAvailable + 0xFFF) & ~0xFFF)];
//             vq->NextBuff = 0;
//             vq->Lock = (ulong)Pointer.Null.Ptr;
//
//             IOPort.Write32(dev->IoBase+0x08, (uint)buf);
//
//             vq->Available->Flags = 0;
//             return false;
//         }
//     }
//
//     public static void virtio_send_buffer(VirtioDeviceInfo* dev, ushort queueIndex, BufferInfo b)
//         => virtio_send_buffer(dev, queueIndex, new[] { b }, 1);
//     
//     public static void virtio_send_buffer(VirtioDeviceInfo* dev, ushort queueIndex, BufferInfo[] b, ulong count)
//     {
//
//         fixed (VirtQueue* vq = &(dev->Queues[queueIndex]))
//         {
//             ulong i;
//             Mutex.Lock(&vq->Lock);
//             
//             ushort index = (ushort)(vq->Available->Index % vq->QueueSize);
//             ushort buffer_index = vq->NextBuff;
//             ushort next_buffer_index;
//             
//             byte* buf = (byte*)(&vq->Buffer[vq->ChunkSize * buffer_index]);
//             Pointer buf2 = Pointer.MakeFrom(buf);
//             
//             vq->Available->Rings[index] = buffer_index;
//             for (i = 0; i < count; i++)
//             {
//                 next_buffer_index = (ushort)((buffer_index + 1) % vq->QueueSize);
//                 fixed (BufferInfo* bi = &b[i])
//                 {
//                 
//                     vq->QueueBuffer[buffer_index].Flags = bi->Flags;
//                     if (i != (count - 1)) vq->QueueBuffer[buffer_index].Flags |= (ushort)DescFlags.Next;
//             
//                     vq->QueueBuffer[buffer_index].Next = next_buffer_index;
//                     vq->QueueBuffer[buffer_index].Length = (uint)bi->Size;
//                     if (bi->Copy)
//                     {
//                         vq->QueueBuffer[buffer_index].Address = (ulong)buf2;
//                         if (!bi->Buffer.Equals(Pointer.Null)) // dont know id null check is right here
//                         {
//                             bi->Buffer.CopyTo();
//                             MemoryOperations.Copy(bi->Buffer, buf2, (int)bi->Size);
//                         }
//
//                         buf2 += bi->Size;
//                     }
//                     else
//                     {
//                         // calling function wants to keep same buffer
//                         vq->QueueBuffer[buffer_index].Address = (ulong)bi->Buffer;
//                     }
//                 }
//             
//                 buffer_index = next_buffer_index;
//             }
//             
//             vq->NextBuff = buffer_index;
//             
//             vq->Available->Index++;
//             IOPort.Write16(dev->IoBase+0x10, queueIndex);
//
//             Mutex.Free(&vq->Lock);
//         }
//     }
//
//     public static void virtio_disable_interrupts(VirtQueue* vq)
//     {
//         vq->Used->Flags = 0;
//     }
//
//     public static void virtio_enable_interrupts(VirtQueue* vq)
//     {
//         vq->Used->Flags = 1;
//     }
//     
//
// }