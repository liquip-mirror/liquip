namespace Liquip.Driver.VirtIO;

public enum DeviceTypeVirtIO : ushort
{
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
    RDMA_device = 4160 + 42
}

public static class DeviceTypeVirtIOEx
{
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
