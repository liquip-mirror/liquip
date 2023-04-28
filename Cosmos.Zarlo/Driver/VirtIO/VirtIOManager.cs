using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Cosmos.Core;
using Cosmos.Core.Memory;
using Cosmos.HAL;
using Cosmos.Zarlo.Memory;

namespace Cosmos.Zarlo.Driver.VirtIO;

public unsafe static class VirtIOManager
{
    private static BaseVirtIODevice[]? _devices = null;
    public static BaseVirtIODevice[] GetDevices()
    {
        if (_devices != null)
        {
            return _devices;
        }

        var output = new List<BaseVirtIODevice>();
        foreach (PCIDevice device in PCI.Devices)
        {
            if (device.VendorID == 0x1AF4)
            {
                try
                {
                    // device.Claimed = true;
                    BaseVirtIODevice d; // = new BaseVirtIODevice(device);
                    switch ((DeviceTypeVirtIO)device.DeviceID)
                    {
                        // case(DeviceTypeVirtIO.EntropySource):
                        //     d = new Entropy.EntropyVirtIO(device);
                        //     break;
                        // case(DeviceTypeVirtIO.GPU_device):
                        //     d = new GPU.GPUVirtIODevice(device);
                        //     break;
                        default:
                            d = new BaseVirtIODevice(device);
                            break;
                    }
                    output.Add(d);
                    d.Initialization();
                }
                catch (Exception ex)
                {
                    // device.Claimed = false;
                }

            }
        }

        _devices = output.ToArray();
        return _devices;
    }



}