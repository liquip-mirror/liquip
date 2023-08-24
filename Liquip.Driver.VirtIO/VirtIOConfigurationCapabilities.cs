namespace Liquip.Driver.VirtIO;

public enum VirtIOConfigurationCapabilities: byte
{
    Common = 1,
    Notify = 2,
    ISR = 3,
    Device = 4,
    PCI = 5,
    SharedMemory = 8,
    Vendor = 9
}
