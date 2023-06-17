using Cosmos.HAL;

namespace Zarlo.Cosmos.Common.Driver;

public readonly struct DeviceCapability
{
	public readonly byte Capability;
	public readonly byte Offset;

	public DeviceCapability(byte capability, byte offset)
	{
		Capability = capability;
		Offset = offset;
	}
}

public class DeviceBase: PCIDevice
{ 

	public DeviceCapability[] Capabilities { get; protected set; }

    public DeviceBase(uint bus, uint slot, uint function) : base(bus, slot, function)
    {
		SetUp();
    }

    public DeviceBase(PCIDevice device) : base(device.bus, device.slot, device.function)
    {
        SetUp();
    }

    void SetUp()
    { 
		Console.WriteLine("looking for capability: {0}", Status);
		if ((Status & (byte)(1 << 4)) != 0)
		{
			var capabilities = new List<DeviceCapability>();
			
			var ptr = ReadRegister8((byte)Config.CapabilityPointer);

			while (ptr != 0)
			{
				var capability = ReadRegister8(ptr);
				Console.WriteLine(capability);

				capabilities.Add(new DeviceCapability(capability, ptr));

				ptr = ReadRegister8((byte)(ptr + 1));
			}

			Capabilities = capabilities.ToArray();
		}
	}


}