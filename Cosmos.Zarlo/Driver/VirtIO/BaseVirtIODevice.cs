using System.Text;
using Cosmos.Core;
using Cosmos.HAL;
using Cosmos.Zarlo.Logger.Interfaces;
using Cosmos.Zarlo.Memory;

namespace Cosmos.Zarlo.Driver.VirtIO;


public class BaseVirtIODevice : DeviceBase
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

    public void SetUp()
    { 
        
    }

    public virtual void Initialization() 
    {
        // INTs.SetIrqHandler(InterruptLine, HandleInterrupt);
        // Console.WriteLine(DebugInfo());
    }


    public virtual void HandleInterrupt(ref INTs.IRQContext aContext)
    { 

    }

    public void Wait(int queue, int timeoutMs = 300)
    { 
        while (true)
        {
            
        }
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

    VirtIoQueue[] virtqueues = new VirtIoQueue[32];

    public VirtIoQueue GetVirtqueue(uint index) => virtqueues[index];

    public void SetVirtqueueBuffer(uint index, ref VirtIoQueue buffer)
    {
        virtqueues[index] = buffer;
        unsafe
        {
            IOPort.Write32(BAR() + VirtIORegisters.QueueSelect, index);
            IOPort.Write32(BAR() + VirtIORegisters.QueueAddress, GCImplementation.GetSafePointer(buffer)); 
        }
        
    }


    public ushort SendCommand(uint index, ref Pointer command, DescFlags descFlags)
    {
        var payload = new VirtQDescriptor(command, descFlags);
        return SendCommand(index, ref payload, descFlags);
    }

    public ushort SendCommand<T>(uint index, ref T command, DescFlags descFlags) where T: struct
    {
        unsafe
        {
            var pointer = Pointer.MakeFrom<T>(command, false);

            return SendCommand(index, ref pointer, descFlags);
        }
    }

    public ushort SendCommand(uint index, ref VirtQDescriptor command, DescFlags descFlags)
    {
        var queue = virtqueues[index];
        var head = queue.NextDescriptor();
        queue.DescriptorWrite(head, ref command);
        return 0;
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
        try
        {
            var sb = new StringBuilder();

            sb.AppendLine($@"BAR: {BAR()}, BAR0: {BAR0}, bus: {bus}, slot: {slot} ");
            sb.AppendLine($@"DeviceID: {DeviceID}, DeviceType: {DeviceType.AsString()}");
            sb.AppendLine($@"BAR1: {BARAddress(1)}, BAR2: {BARAddress(2)}, BAR3: {BARAddress(3)}, BAR4: {BARAddress(4)}, BAR5: {BARAddress(5)},");
            sb.AppendLine($@"BAR6: {BARAddress(6)}, BAR7: {BARAddress(7)}, BAR8: {BARAddress(8)}, BAR9: {BARAddress(9)}, BAR10: {BARAddress(10)},");
            sb.AppendLine($@"BAR11: {BARAddress(11)}, BAR12: {BARAddress(12)}, BAR13: {BARAddress(13)}, BAR14: {BARAddress(14)}, BAR15: {BARAddress(15)}");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }


    }
    

}


