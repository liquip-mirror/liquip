
using Cosmos.HAL;

namespace Zarlo.Cosmos.Driver.VirtIO.Block;

public class BlockVirtIODevice: BaseVirtIODevice
{

    void Check()
    { 
        if(DeviceID != (ushort)(DeviceTypeVirtIO.BlockDevice)) throw new NotSupportedException(string.Format("wrong DeviceID {0}", DeviceID));
    }


    public BlockVirtIODevice(PCIDevice device) : base(device)
    {
        Check();
    }

    public BlockVirtIODevice(uint bus, uint slot, uint function) : base(bus, slot, function)
    {
        Check();
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
    public Int16 cylinders { get; set; }
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
    public Int16 min_io_size { get; set; }
    // optimal (suggested maximum) I/O size in blocks
    public Int16 opt_io_size { get; set; }
}

public struct BlockVirtIODeviceConfig
{
    public Int64 capacity { get; set; }
    public Int32 size_max { get; set; }
    public Int32 seg_max { get; set; }

    public virtio_blk_geometry virtio_blk_geometry { get; set; }

    public Int32 blk_size { get; set; }

    public virtio_blk_topology virtio_blk_topology { get; set; }

    public byte writeback { get; set; }
    public byte unused0 { get; set; }
    public Int16 num_queues { get; set; }
    public Int32 max_discard_sectors { get; set; }
    public Int32 max_discard_seg { get; set; }
    public Int32 discard_sector_alignment { get; set; }
    public Int32 max_write_zeroes_sectors { get; set; }
    public Int32 max_write_zeroes_seg { get; set; }
    public byte write_zeroes_may_unmap { get; set; }
    public byte unused1 { get; set; }
    public byte unused2 { get; set; }
    public byte unused3 { get; set; }
    public Int32 max_secure_erase_sectors { get; set; }
    public Int32 max_secure_erase_seg { get; set; }
    public Int32 secure_erase_sector_alignment { get; set; }
}