// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

using System;

namespace Liquip.Driver.VirtIO;

[Flags]
public enum GPUDeviceFeatureFlag : byte
{
    VIRGL = 1,
    EDID = 1 << 1
}

[Flags]
public enum DeviceStatusFlag : byte
{
    ACKNOWLEDGE = 1,
    DRIVER = 2,
    DRIVER_OK = 4,
    FEATURES_OK = 8,
    FAILED = 128
}

[Flags]
public enum DeviceFeatureFlag : uint
{
    RING_INDIRECT_DESC = 1 << 28,
    RING_EVENT_IDX = 1 << 29,
    VERSION = 32768
}

[Flags]
public enum NetDeviceFeatureFlag : ulong
{
    CSUM = 1,
    GUEST_CSUM = 1 << 1,
    CTRL_GUEST_OFFLOADS = 1 << 2,
    MAC = 1 << 5,
    GUEST_TSO4 = 1 << 7,
    GUEST_TSO6 = 1 << 8,
    GUEST_ECN = 1 << 9,
    GUEST_UFO = 1 << 10,
    HOST_TSO4 = 1 << 11,
    HOST_TSO6 = 1 << 12,
    HOST_ECN = 1 << 13,
    HOST_UFO = 1 << 14,
    MRG_RXBUF = 1 << 15,
    STATUS = 1 << 16,
    CTRL_VQ = 1 << 17,
    CTRL_RX = 1 << 18,
    CTRL_VLAN = 1 << 19,
    GUEST_ANNOUNCE = 1 << 21,
    MQ = 1 << 22,
    CTRL_MAC_ADDR = (ulong)1 << 23,
    HOST_USO = (ulong)1 << 56,
    HASH_REPORT = (ulong)1 << 57,
    GUEST_HDRLEN = (ulong)1 << 59,
    RSS = (ulong)1 << 60,
    RSC_EXT = (ulong)1 << 61,
    STANDBY = (ulong)1 << 62,
    SPEED_DUPLEX = (ulong)1 << 63
}

[Flags]
public enum BlockDeviceFeatureFlag : uint
{
    // Maximum size of any single segment is in size_max.
    SIZE_MAX = 1,

    // Maximum number of segments in a request is in seg_max.
    SEG_MAX = 1 << 2,

    // Disk-style geometry specified in geometry
    GEOMETRY = 1 << 4,

    // Device is read-only.
    RO = 1 << 5,

    // Block size of disk is in blk_size.
    BLK_SIZE = 1 << 6,

    // Cache flush command support.
    FLUSH = 1 << 9,

    // Device exports information on optimal I/O alignment.
    TOPOLOGY = 1 << 10,

    // Device can toggle its cache between writeback and writethrough modes.
    CONFIG_WCE = 1 << 11,

    // Device supports multiqueue.
    MQ = 1 << 12,

    //Device can support discard command, maximum discard sectors size in
    //max_discard_sectors and maximum discard segment number in max_discard_seg.
    DISCARD = 1 << 13,

    // Device can support write zeroes command, maximum write zeroes
    // sectors size in max_write_zeroes_sectors and maximum write zeroes segment number in max_write_-
    // zeroes_seg.
    WRITE_ZEROES = 1 << 14,

    // Device supports providing storage lifetime information.
    LIFETIME = 1 << 15,

    // Device supports secure erase command, maximum erase sectors
    // count in max_secure_erase_sectors and maximum erase segment number in max_secure_erase_seg.
    SECURE_ERASE = 1 << 16
}

[Flags]
public enum ConsoleDeviceFeatureFlag : uint
{
    SIZE = 1,
    MULTIPORT = 1 << 1
}

[Flags]
public enum EntropyDeviceFeatureFlag : uint
{
}

[Flags]
public enum MemoryBalloonDeviceFeatureFlag : uint
{
    MUST_TELL_HOST = 1,
    STATS_VQ = 1 << 1
}

[Flags]
public enum SCSIHostDeviceFeatureFlag : uint
{
    INOUT = 1,
    HOTPLUG = 1 << 1,
    CHANGE = 1 << 2
}

[Flags]
public enum DescFlags : ushort
{
    Next = 1,
    WriteOnly = 1 << 1,
    Indirect = 1 << 2
}

public enum VIRTIO_PCI_CAP_ISR_CFG
{
    /* Common configuration */
    COMMON_CFG = 1,

    /* Notifications */
    NOTIFY_CFG = 2,

    /* ISR Status */
    ISR_CFG = 3,

    /* Device specific configuration */
    DEVICE_CFG = 4,

    /* PCI configuration access */
    PCI_CFG = 5,

    /* Shared memory region */
    SHARED_MEMORY_CFG = 8,

    /* Vendor-specific data */
    VENDOR_CFG = 9
}
