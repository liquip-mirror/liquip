// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Cosmos.Zarlo.Driver.VirtIO;

[Flags]
public enum GPUDeviceFeatureFlag : byte
{
    VIRGL = 1,
    EDID = 1 << 1
}
    
    
[Flags]
public enum DeviceStatusFlag : byte
{
    ACKNOWLEDGE = 1 ,
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
public enum NetDeviceFeatureFlag : uint
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
}

[Flags]
public enum BlockDeviceFeatureFlag : uint
{
    SIZE_MAX = 1,
    SEG_MAX = 1 << 1,
    GEOMETRY = 1 << 4,
    RO = 1 << 5,
    BLK_SIZE = 1 << 6,
    TOPOLOGY = 1 << 10,
}

[Flags]
public enum ConsoleDeviceFeatureFlag : uint
{
    SIZE = 1,
    MULTIPORT = 1 << 1,
}

[Flags]
public enum EntropyDeviceFeatureFlag : uint
{
}

[Flags]
public enum MemoryBalloonDeviceFeatureFlag : uint
{
    MUST_TELL_HOST = 1,
    STATS_VQ = 1 << 1,
}

[Flags]
public enum SCSIHostDeviceFeatureFlag : uint
{
    INOUT = 1,
    HOTPLUG = 1 << 1,
    CHANGE = 1 << 2,
}

[Flags]
public enum DescFlags : ushort
{
    Next = 1,
    WriteOnly = 1 << 1,
    Indirect  = 1 << 2
}