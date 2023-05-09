using System.Text;
using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.Zarlo.Logger.Interfaces;
using Cosmos.Zarlo.Memory;

namespace Cosmos.Zarlo.Driver.VirtIO;

public enum DeviceTypeVirtIO: ushort { 

    Reserved = 4160 + 0,
    NetworkCard = 4160 + 1,
    BlockDevice = 4160 + 2,
    Console = 4160 + 3,
    EntropySource = 4160 + 4,
    MemoryBallooning = 4160 + 5,
    IoMemory = 4160 + 6,
    Rpmsg = 4160 + 7,
    SCSIhost = 4160 + 8,
    _9PTransport = 4160 + 9,
    Mac80211Wlan = 4160 + 10,
    RprocSerial = 4160 + 11,
    VirtioCAIF = 4160 + 12,
    MemoryBalloon = 4160 + 13,
    GPU_device = 4160 + 16,
    Timer_ClockDevice = 4160 + 17,
    InputDevice = 4160 + 18,
    SocketDevice = 4160 + 19,
    CryptoDevice = 4160 + 20,
    SignalDistributionModule = 4160 + 21,
    PstoreDevice = 4160 + 22,
    IOMMU_device = 4160 + 23,
    MemoryDevice = 4160 + 24,
    AudioDevice = 4160 + 25,
    FileSystemDevice = 4160 + 26,
    PMEMDevice = 4160 + 27,
    RPMBDevice = 4160 + 28,
    Mac80211HwsimWirelessSimulationDevice = 4160 + 29,
    VideoEncoderDevice = 4160 + 30,
    VideoDecoderDevice = 4160 + 31,
    SCMIDevice = 4160 + 32,
    NitroSecureModule = 4160 + 33,
    I2C_adapter = 4160 + 34,
    Watchdog = 4160 + 35,
    CAN_device = 4160 + 36,
    ParameterServer = 4160 + 38,
    AudioPolicyDevice = 4160 + 39,
    BluetoothDevice = 4160 + 40,
    GPIO_device = 4160 + 41,
    RDMA_device = 4160 + 42,

}

public static class DeviceTypeVirtIOEx {

    public static string AsString(this DeviceTypeVirtIO me)
    { 
        switch (me)
        {
           case DeviceTypeVirtIO.Reserved:
            return "Reserved";
           case DeviceTypeVirtIO.NetworkCard:
            return "NetworkCard";
           case DeviceTypeVirtIO.BlockDevice:
            return "BlockDevice";
           case DeviceTypeVirtIO.Console:
            return "Console";
           case DeviceTypeVirtIO.EntropySource:
            return "EntropySource";
           case DeviceTypeVirtIO.MemoryBallooning:
            return "MemoryBallooning";
           case DeviceTypeVirtIO.IoMemory:
            return "IoMemory";
           case DeviceTypeVirtIO.Rpmsg:
            return "Rpmsg";
           case DeviceTypeVirtIO.SCSIhost:
            return "SCSIhost";
           case DeviceTypeVirtIO._9PTransport:
            return "_9PTransport";
           case DeviceTypeVirtIO.Mac80211Wlan:
            return "Mac80211Wlan";
           case DeviceTypeVirtIO.RprocSerial:
            return "RprocSerial";
           case DeviceTypeVirtIO.VirtioCAIF:
            return "VirtioCAIF";
           case DeviceTypeVirtIO.MemoryBalloon:
            return "MemoryBalloon";
           case DeviceTypeVirtIO.GPU_device:
            return "GPU_device";
           case DeviceTypeVirtIO.Timer_ClockDevice:
            return "Timer_ClockDevice";
           case DeviceTypeVirtIO.InputDevice:
            return "InputDevice";
           case DeviceTypeVirtIO.SocketDevice:
            return "SocketDevice";
           case DeviceTypeVirtIO.CryptoDevice:
            return "CryptoDevice";
           case DeviceTypeVirtIO.SignalDistributionModule:
            return "SignalDistributionModule";
           case DeviceTypeVirtIO.PstoreDevice:
            return "PstoreDevice";
           case DeviceTypeVirtIO.IOMMU_device:
            return "IOMMU_device";
           case DeviceTypeVirtIO.MemoryDevice:
            return "MemoryDevice";
           case DeviceTypeVirtIO.AudioDevice:
            return "AudioDevice";
           case DeviceTypeVirtIO.FileSystemDevice:
            return "FileSystemDevice";
           case DeviceTypeVirtIO.PMEMDevice:
            return "PMEMDevice";
           case DeviceTypeVirtIO.RPMBDevice:
            return "RPMBDevice";
           case DeviceTypeVirtIO.Mac80211HwsimWirelessSimulationDevice:
            return "Mac80211HwsimWirelessSimulationDevice";
           case DeviceTypeVirtIO.VideoEncoderDevice:
            return "VideoEncoderDevice";
           case DeviceTypeVirtIO.VideoDecoderDevice:
            return "VideoDecoderDevice";
           case DeviceTypeVirtIO.SCMIDevice:
            return "SCMIDevice";
           case DeviceTypeVirtIO.NitroSecureModule:
            return "NitroSecureModule";
           case DeviceTypeVirtIO.I2C_adapter:
            return "I2C_adapter";
           case DeviceTypeVirtIO.Watchdog:
            return "Watchdog";
           case DeviceTypeVirtIO.CAN_device:
            return "CAN_device";
           case DeviceTypeVirtIO.ParameterServer:
            return "ParameterServer";
           case DeviceTypeVirtIO.AudioPolicyDevice:
            return "AudioPolicyDevice";
           case DeviceTypeVirtIO.BluetoothDevice:
            return "BluetoothDevice";
           case DeviceTypeVirtIO.GPIO_device:
            return "GPIO_device";
           case DeviceTypeVirtIO.RDMA_device:
            return "RDMA_device";

            default:
                return "unknown" + (int)me;
        }
    }

}


public class BaseVirtIODevice : PCIDevice
{

    public const int DeviceIDOffset = 4160;


    public DeviceTypeVirtIO DeviceType => (DeviceTypeVirtIO)DeviceID;

    private void Check()
    {
        if (VendorID != 0x1AF4) throw new NotSupportedException(string.Format("wrong VendorID {0}", VendorID));
    }

    public int BAR()
    {
        for (int i = 0; i < BaseAddressBar.Length; i++)
        {
            if (BaseAddressBar[i].BaseAddress != 0)
                return (int)BaseAddressBar[i].BaseAddress;
        }
        return 0;
    }

    public BaseVirtIODevice(uint bus, uint slot, uint function) : base(bus, slot, function)
    {

    }

    // private ILogger _logger = Logger.Log.GetLogger<BaseVirtIODevice>();

    public BaseVirtIODevice(PCIDevice device) : base(device.bus, device.slot, device.function)
    {

    }

    public virtual void Initialization() 
    {
        Console.WriteLine(DebugInfo());
    }


    public void SetDeviceStatusFlag(DeviceStatusFlag flag)
    {
        IOPort.Write8(BAR() + VirtIORegisters.DeviceStatus, (byte)flag);
    }

    public DeviceStatusFlag GetDeviceStatusFlag()
    {
        return (DeviceStatusFlag)IOPort.Read8(BAR() + VirtIORegisters.DeviceStatus);
    }


    public void SetDeviceFeaturesFlag(byte flag)
    {
        IOPort.Write8(BAR() + VirtIORegisters.DeviceFeatures, flag);
    }

    public byte GetDeviceFeaturesFlag()
    {
        return IOPort.Read8(BAR() + VirtIORegisters.DeviceFeatures);
    }



    /// <summary>
    /// call then device is found
    /// </summary>
    public void ACKNOWLEDGE()
    {
        Console.WriteLine($@"ACKNOWLEDGE");
        var flags = DeviceStatusFlag.ACKNOWLEDGE;
        SetDeviceStatusFlag(flags);
    }

    /// <summary>
    /// call then driver FEATURES locked in
    /// </summary>
    public void FEATURES_OK()
    {
        var flags = DeviceStatusFlag.FEATURES_OK;
        SetDeviceStatusFlag(flags);
    }

    /// <summary>
    /// check if device supports features requested by the driver
    /// </summary>
    public bool IS_FEATURES_OK()
    {
        return IsBitSet((byte)GetDeviceStatusFlag(), 4);
    }

    bool IsBitSet(byte b, int pos)
    {
        return (b & (1 << pos)) != 0;
    }

    /// <summary>
    /// call then driver is done loading
    /// </summary>
    public void DRIVER_OK()
    {
        var flags = GetDeviceStatusFlag() | DeviceStatusFlag.DRIVER_OK;
        SetDeviceStatusFlag(flags);
    }

    /// <summary>
    /// call then device driver is loaded
    /// </summary>
    public void DRIVER()
    {
        var flags = GetDeviceStatusFlag() | DeviceStatusFlag.DRIVER;
        SetDeviceStatusFlag(flags);
    }

    /// <summary>
    /// reset device
    /// </summary>
    public void RESET()
    {
        SetDeviceStatusFlag(0x00);
    }

    VirtQDesc[] virtqueues = new VirtQDesc[32];

    public VirtQDesc GetVirtqueueBuffer(uint index) => virtqueues[index];

    public void SetVirtqueueBuffer(uint index, ref VirtQDesc buffer)
    {
        virtqueues[index] = buffer;
        unsafe
        {
            IOPort.Write32(BAR() + 0x0E, index);
            IOPort.Write32(BAR() + 0x08, GCImplementation.GetSafePointer(buffer)); 
        }
        
    }

    protected uint BARAddress(int i) {
        if (BaseAddressBar.Length <= i)
        {
            return 0;
        }
        return BaseAddressBar[i].BaseAddress;
    }

    public string DebugInfo()
    {

        var sb = new StringBuilder();

        sb.AppendLine($@"BAR: {BAR()}, BAR0: {BAR0}, bus: {bus}, slot: {slot} ");
        sb.AppendLine($@"DeviceID: {DeviceID}, DeviceType: {DeviceType.AsString()}");
        sb.AppendLine($@"BAR1: {BARAddress(1)}, BAR2: {BARAddress(2)}, BAR3: {BARAddress(3)}, BAR4: {BARAddress(4)}, BAR5: {BARAddress(5)},");
        sb.AppendLine($@"BAR6: {BARAddress(6)}, BAR7: {BARAddress(7)}, BAR8: {BARAddress(8)}, BAR9: {BARAddress(9)}, BAR10: {BARAddress(10)},");
        sb.AppendLine($@"BAR11: {BARAddress(11)}, BAR12: {BARAddress(12)}, BAR13: {BARAddress(13)}, BAR14: {BARAddress(14)}, BAR15: {BARAddress(15)}");

        return sb.ToString();

    }
    

}


public struct VirtQDesc
{

    public unsafe VirtQDesc(Pointer ptr, DescFlags flag) {
        addr = (UInt64)ptr.Ptr;
        len = ptr.Size;
        flags = flag;
        next = 0;
    }

    /* Address (guest-physical). */
    UInt64 addr;
    /* Length. */
    UInt32 len;

    /* The flags as indicated above. */
    DescFlags flags;
    /* Next field if flags & NEXT */
    UInt16 next;

    public Pointer GetPointer()
    {
        unsafe
        {
            return Pointer.MakeFrom((uint*)addr, len, false);
        }
    }

    public bool HasNext()
    {
        return flags.HasFlag(DescFlags.Next);
    }
}

public struct VirtiIoPciCap
{
    byte cap_vndr { get; set; } /* Generic PCI field: PCI_CAP_ID_VNDR */
    byte cap_next { get; set; } /* Generic PCI field: next ptr. */
    byte cap_len { get; set; } /* Generic PCI field: capability length */
    byte cfg_type { get; set; } /* Identifies the structure. */
    byte bar { get; set; } /* Where to find it. */
    byte id { get; set; } /* Multiple capabilities of the same type */
    byte paddin0 { get; set; } /* Pad to full dword. */
    byte paddin1 { get; set; } /* Pad to full dword. */
    UInt32 offset { get; set; } /* Offset within bar. */
    UInt32 length { get; set; } /* Length of the structure, in bytes. */
}

