using System;
using System.Text;
using Cosmos.Core;
using Cosmos.HAL;
using Liquip.Common.Driver;
using Liquip.Memory;

namespace Liquip.Driver.VirtIO;

public class BaseVirtIODevice : PCIDriverBase
{
    public const int DeviceIDOffset = 4160;

    public Pointer Bar { get; private set; }
    public uint Offset { get; private set; }

    public Pointer NotifyCapability { get; private set; }
    public uint NotifyCapabilityOffset { get; private set; }
    public uint NotifyOffMultiplier { get; private set; }

    public Pointer DeviceConfigurationPointer { get; private set; }
    public uint DeviceConfigurationOffset { get; private set; }


    private readonly VirtIoQueue[] virtqueues = new VirtIoQueue[32];

    public BaseVirtIODevice(uint bus, uint slot, uint function) : base(bus, slot, function)
    {
        SetUp();
    }

    // private ILogger _logger = Logger.Log.GetLogger<BaseVirtIODevice>();

    public BaseVirtIODevice(PCIDevice device) : base(device.bus, device.slot, device.function)
    {
        SetUp();
    }

    private void SetUp()
    {

		foreach (var capability in Capabilities)
		{
			if (capability.Capability != 0x09)
				continue;

			var configType = ReadRegister8((byte)(capability.Offset + 3));
			var bar = ReadRegister8((byte)(capability.Offset + 4));
			var offset = ReadRegister32((byte)(capability.Offset + 8));
            var pciBar = BaseAddressBar[bar] ?? null;
			switch ((VirtIOConfigurationCapabilities)configType)
			{
				case VirtIOConfigurationCapabilities.Common:
					{


						if (!pciBar.IsIO)
						{
							Bar = Pointer.MakeFrom((Address)pciBar.BaseAddress, uint.MaxValue);
							Offset = offset;
						}
						else
						{

							return;
						}

						break;
					}
				case VirtIOConfigurationCapabilities.Notify:
					{

						if (!pciBar.IsIO)
						{
							NotifyCapability = Pointer.MakeFrom((Address)pciBar.BaseAddress, uint.MaxValue);
							NotifyCapabilityOffset = offset;
						}
						else
						{
							return;
						}

						NotifyOffMultiplier = ReadRegister32((byte)(capability.Offset + 16));
						break;
					}
				case VirtIOConfigurationCapabilities.ISR: break;
				case VirtIOConfigurationCapabilities.Device:
					{

						if (!pciBar.IsIO)
						{
							DeviceConfigurationPointer = Pointer.MakeFrom((Address)pciBar.BaseAddress, uint.MaxValue);
							DeviceConfigurationOffset = offset;
						}
						else
						{
							return;
						}

						break;
					}
				case VirtIOConfigurationCapabilities.PCI: break;
				case VirtIOConfigurationCapabilities.SharedMemory: break;
				case VirtIOConfigurationCapabilities.Vendor: break;
			}
		}

    }


    public DeviceTypeVirtIO DeviceType => (DeviceTypeVirtIO)DeviceID;

    private void Check()
    {
        if (VendorID != 0x1AF4)
        {
            throw new NotSupportedException(string.Format("wrong VendorID {0}", VendorID));
        }
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
        IOPort.Write8(Bar.ToInt() + VirtIORegisters.DeviceStatus, (byte)flag);
    }

    public DeviceStatusFlag GetDeviceStatusFlag()
    {
        return (DeviceStatusFlag)IOPort.Read8(Bar.ToInt() + VirtIORegisters.DeviceStatus);
    }


    public void SetDeviceFeaturesFlag(byte flag)
    {
        IOPort.Write8(Bar.ToInt() + VirtIORegisters.DeviceFeatures, flag);
    }

    public byte GetDeviceFeaturesFlag()
    {
        return IOPort.Read8(Bar.ToInt() + VirtIORegisters.DeviceFeatures);
    }


    /// <summary>
    ///     call then device is found
    /// </summary>
    public void ACKNOWLEDGE()
    {
        Console.WriteLine(@"ACKNOWLEDGE");
        var flags = DeviceStatusFlag.ACKNOWLEDGE;
        SetDeviceStatusFlag(flags);
    }

    /// <summary>
    ///     call then driver FEATURES locked in
    /// </summary>
    public void FEATURES_OK()
    {
        var flags = DeviceStatusFlag.FEATURES_OK;
        SetDeviceStatusFlag(flags);
    }

    /// <summary>
    ///     check if device supports features requested by the driver
    /// </summary>
    public bool IS_FEATURES_OK()
    {
        return IsBitSet((byte)GetDeviceStatusFlag(), 4);
    }

    private bool IsBitSet(byte b, int pos)
    {
        return (b & (1 << pos)) != 0;
    }

    /// <summary>
    ///     call then driver is done loading
    /// </summary>
    public void DRIVER_OK()
    {
        var flags = GetDeviceStatusFlag() | DeviceStatusFlag.DRIVER_OK;
        SetDeviceStatusFlag(flags);
    }

    /// <summary>
    ///     call then device driver is loaded
    /// </summary>
    public void DRIVER()
    {
        var flags = GetDeviceStatusFlag() | DeviceStatusFlag.DRIVER;
        SetDeviceStatusFlag(flags);
    }

    /// <summary>
    ///     reset device
    /// </summary>
    public void RESET()
    {
        SetDeviceStatusFlag(0x00);
    }

    public VirtIoQueue GetVirtqueue(uint index)
    {
        return virtqueues[index];
    }

    public void SetVirtqueueBuffer(uint index, ref VirtIoQueue buffer)
    {
        virtqueues[index] = buffer;
        IOPort.Write32(Bar.ToInt() + VirtIORegisters.QueueSelect, index);
        IOPort.Write32(Bar.ToInt() + VirtIORegisters.QueueAddress, GCImplementation.GetSafePointer(buffer));
    }


    public ushort SendCommand(uint index, ref Pointer command, DescFlags descFlags)
    {
        var payload = new VirtQDescriptor(command, descFlags);
        return SendCommand(index, ref payload, descFlags);
    }

    public ushort SendCommand<T>(uint index, ref T command, DescFlags descFlags) where T : struct
    {
        // var pointer = Pointer.MakeFrom<T>(command, false);

        // return SendCommand(index, ref pointer, descFlags);
        return 0;
    }

    public ushort SendCommand(uint index, ref VirtQDescriptor command, DescFlags descFlags)
    {
        var queue = virtqueues[index];
        var head = queue.NextDescriptor();
        queue.DescriptorWrite(head, ref command);
        return 0;
    }

    protected uint BARAddress(int i)
    {
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

            sb.AppendLine($@"BAR: {Bar.ToInt()}, BAR0: {BAR0}, bus: {bus}, slot: {slot} ");
            sb.AppendLine($@"DeviceID: {DeviceID}, DeviceType: {DeviceType.AsString()}");
            return sb.ToString();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
}
