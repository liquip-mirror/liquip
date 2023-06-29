// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Zarlo.Cosmos.Driver.VirtIO;

public static class VirtIORegisters
{
    public const int DeviceFeatures = 0x00;
    public const int GuestFeatures = 0x04;
    public const int QueueAddress = 0x08;
    public const int QueueSize = 0x0C;
    public const int QueueSelect = 0x0E;

    public const int QueueNotify = 0x10;
    public const int DeviceStatus = 0x12;
    public const int ISRStatus = 0x13;


    public static class Network
    {
        public const byte MACAddress1 = 0x14;
        public const byte MACAddress2 = 0x15;
        public const byte MACAddress3 = 0x16;
        public const byte MACAddress4 = 0x17;
        public const byte MACAddress5 = 0x18;
        public const byte MACAddress6 = 0x19;
        public const byte Status = 0x1A;
    }

    public static class BlockDevice
    {
        public const byte TotalSectorCount = 0x14;
        public const byte MaximumSegmentSize = 0x1C;
        public const byte MaximumSegmentCount = 0x20;
        public const byte CylinderCount = 0x24;
        public const byte HeadCount = 0x26;
        public const byte SectorCount = 0x27;
        public const byte BlockLength = 0x28;
    }
}
