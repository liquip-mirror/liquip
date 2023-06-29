using System;
using Cosmos.HAL;

namespace Zarlo.Cosmos.Driver.VirtIO.Block;

public class BlockVirtIODevice : BaseVirtIODevice
{
    public BlockVirtIODevice(PCIDevice device) : base(device)
    {
        Check();
    }

    public BlockVirtIODevice(uint bus, uint slot, uint function) : base(bus, slot, function)
    {
        Check();
    }

    private void Check()
    {
        if (DeviceID != (ushort)DeviceTypeVirtIO.BlockDevice)
        {
            throw new NotSupportedException(string.Format("wrong DeviceID {0}", DeviceID));
        }
    }

    public override void Initialization()
    {
        ACKNOWLEDGE();

        var flags = BlockDeviceFeatureFlag.BLK_SIZE;
        SetDeviceFeaturesFlag((byte)flags);
    }
}

public struct virtio_blk_geometry
{
    public short cylinders { get; set; }
    public byte heads { get; set; }
    public byte sectors { get; set; }
}

public struct virtio_blk_topology
{
    // # of logical blocks per physical block (log2)
    public byte physical_block_exp { get; set; }

    // offset of first aligned logical block
    public byte alignment_offset { get; set; }

    // suggested minimum I/O size in blocks
    public short min_io_size { get; set; }

    // optimal (suggested maximum) I/O size in blocks
    public short opt_io_size { get; set; }
}

public struct BlockVirtIODeviceConfig
{
    public long capacity { get; set; }
    public int size_max { get; set; }
    public int seg_max { get; set; }

    public virtio_blk_geometry virtio_blk_geometry { get; set; }

    public int blk_size { get; set; }

    public virtio_blk_topology virtio_blk_topology { get; set; }

    public byte writeback { get; set; }
    public byte unused0 { get; set; }
    public short num_queues { get; set; }
    public int max_discard_sectors { get; set; }
    public int max_discard_seg { get; set; }
    public int discard_sector_alignment { get; set; }
    public int max_write_zeroes_sectors { get; set; }
    public int max_write_zeroes_seg { get; set; }
    public byte write_zeroes_may_unmap { get; set; }
    public byte unused1 { get; set; }
    public byte unused2 { get; set; }
    public byte unused3 { get; set; }
    public int max_secure_erase_sectors { get; set; }
    public int max_secure_erase_seg { get; set; }
    public int secure_erase_sector_alignment { get; set; }
}
