using Cosmos.HAL;
using Cosmos.Zarlo.Logger.Interfaces;
using Cosmos.Zarlo.Memory;
using XSharp.Tokens;

namespace Cosmos.Zarlo.Driver.VirtIO.Entropy;

public class EntropyVirtIO : BaseVirtIODevice
{

    void Check()
    { 
        if(DeviceID != (ushort)(DeviceTypeVirtIO.EntropySource)) throw new NotSupportedException(string.Format("wrong DeviceID {0}", DeviceID));
    }

    // private ILogger _logger = Logger.Log.GetLogger<EntropyVirtIO>();

    public EntropyVirtIO(PCIDevice device) : base(device)
    {
        Check();
    }

    public EntropyVirtIO(uint bus, uint slot, uint function) : base(bus, slot, function)
    {
        Check();
    }

    public void GetEntropy(ref Pointer pointer)
    {
        var buffer = new VirtQDesc(pointer, DescFlags.WriteOnly);
        SetVirtqueueBuffer(0, ref buffer);
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