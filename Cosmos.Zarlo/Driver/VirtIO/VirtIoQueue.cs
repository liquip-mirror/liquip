using Cosmos.Zarlo.Memory;

namespace Cosmos.Zarlo.Driver.VirtIO;


public class VirtIoQueue
{

    public const byte RingFlags = 0;
    public const byte RingIndex = 2;
    public const byte RingRings = 4;
    public const byte RingUsedEvent = 6;


    public ushort Size { get; }

    public uint DescriptorSize { get; }
    public uint AvailableSize { get; }
    public uint UsedSize { get; }

    public Pointer Buffer { get; }
    public uint BufferSize { get; }

    private ushort index;

    public VirtIoQueue(ushort size)
    {
        Size = size;

        DescriptorSize = (uint)(16 * size);
        AvailableSize = (uint)(6 + 2 * size);
        UsedSize = (uint)(6 + 8 * size);

        BufferSize = DescriptorSize + AvailableSize + UsedSize;
        Buffer = Pointer.New(BufferSize, false);

        for (uint i = 0; i < size - 1; i++)
            DescriptorWrite16(i, VirtQDescriptor.NextDescIdx, (ushort)(i + 1));
        
        DescriptorWrite16((uint)(size - 1), VirtQDescriptor.NextDescIdx, 0xFFFF); // Last entry in the list, point it to an invalid value

        index = 0;
    }

    public ushort NextDescriptor()
    {
        return index++;
    }

    public ushort CurrentDescriptor()
    {
        return index;
    }

    public void SetHead(ushort head)
    {
        var availableIndex = AvailableRingRead16(VirtIoQueue.RingIndex);

        if (availableIndex == ushort.MaxValue)
            availableIndex = 0;

        AvailableRingWrite16((uint)(VirtIoQueue.RingRings + availableIndex % Size), head);
        AvailableRingWrite16(VirtIoQueue.RingIndex, (ushort)(availableIndex + 1));

        index = 0;

    }

    public void DescriptorWrite(uint descriptor, ref VirtQDescriptor value)
    {
        DescriptorWrite64(descriptor, VirtQDescriptor.Phys, value.addr);
        DescriptorWrite32(descriptor, VirtQDescriptor.Len, value.len);
        DescriptorWrite16(descriptor, VirtQDescriptor.Flags, (ushort)value.flags);
    }

    public void DescriptorWrite16(uint descriptor, uint offset, ushort value)
    {
        unsafe
        {
            *(ushort*)Buffer.Ptr[descriptor * 16 + offset] = value;
        }
    }

    public void DescriptorWrite32(uint descriptor, uint offset, uint value)
    {
        unsafe
        {
            Buffer.Ptr[descriptor * 16 + offset] = value;
        }
    }

    public void DescriptorWrite64(uint descriptor, uint offset, ulong value)
    {
        unsafe
        {
            *(ulong*)Buffer.Ptr[descriptor * 16 + offset] = value;
        }    
    }

    public ushort AvailableRingRead16(uint offset)
    {
        unsafe
        {
            return (ushort)Buffer.Ptr[(DescriptorSize + offset)];
        }
    }

    public void AvailableRingWrite16(uint offset, ushort value)
    {
        unsafe
        {
            *(ushort*)Buffer.Ptr[DescriptorSize + offset] = value;
        }
    }

    public ushort UsedRingRead16(uint offset)
    {
        unsafe
        {
            return (ushort)Buffer.Ptr[(AvailableSize + offset)];
        }
    }
}
