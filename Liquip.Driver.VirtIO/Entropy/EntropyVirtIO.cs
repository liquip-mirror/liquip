using System;
using Cosmos.HAL;

namespace Liquip.Driver.VirtIO.Entropy;

public class EntropyVirtIO : BaseVirtIODevice
{
    // private ILogger _logger = Logger.Log.GetLogger<EntropyVirtIO>();

    public EntropyVirtIO(PCIDevice device) : base(device)
    {
        Check();
    }

    public EntropyVirtIO(uint bus, uint slot, uint function) : base(bus, slot, function)
    {
        Check();
    }

    private void Check()
    {
        if (DeviceID != (ushort)DeviceTypeVirtIO.EntropySource)
        {
            throw new NotSupportedException(string.Format("wrong DeviceID {0}", DeviceID));
        }
    }


    public override void Initialization()
    {
        // _logger.Info("Initialization");
        ACKNOWLEDGE();
        FEATURES_OK();
        DRIVER();
        DRIVER_OK();
        // _logger.Info("Initialization Done");
    }
}
