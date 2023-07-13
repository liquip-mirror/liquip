using System;
using System.Collections.Generic;
using Cosmos.HAL;
using Liquip.Driver.VirtIO.Entropy;
using Liquip.Driver.VirtIO.GPU;

namespace Liquip.Driver.VirtIO;

public static class VirtIOManager
{
    private static BaseVirtIODevice[]? _devices;
    private static BaseVirtIODevice[]? _devices_filtered;

    public static BaseVirtIODevice[] GetDevices(bool filtered)
    {
        if (filtered == false)
        {
            return GetDevices();
        }

        if (_devices_filtered != null)
        {
            return _devices_filtered;
        }

        var output = new List<BaseVirtIODevice>();
        foreach (var item in GetDevices())
        {
            switch (item.DeviceType)
            {
                case DeviceTypeVirtIO.Reserved:
                case DeviceTypeVirtIO.NetworkCard:
                case DeviceTypeVirtIO.BlockDevice:
                case DeviceTypeVirtIO.Console:
                case DeviceTypeVirtIO.EntropySource:
                case DeviceTypeVirtIO.MemoryBallooning:
                case DeviceTypeVirtIO.IoMemory:
                case DeviceTypeVirtIO.Rpmsg:
                case DeviceTypeVirtIO.SCSIhost:
                case DeviceTypeVirtIO._9PTransport:
                case DeviceTypeVirtIO.Mac80211Wlan:
                case DeviceTypeVirtIO.RprocSerial:
                case DeviceTypeVirtIO.VirtioCAIF:
                case DeviceTypeVirtIO.MemoryBalloon:
                case DeviceTypeVirtIO.GPU_device:
                case DeviceTypeVirtIO.Timer_ClockDevice:
                case DeviceTypeVirtIO.InputDevice:
                case DeviceTypeVirtIO.SocketDevice:
                case DeviceTypeVirtIO.CryptoDevice:
                case DeviceTypeVirtIO.SignalDistributionModule:
                case DeviceTypeVirtIO.PstoreDevice:
                case DeviceTypeVirtIO.IOMMU_device:
                case DeviceTypeVirtIO.MemoryDevice:
                case DeviceTypeVirtIO.AudioDevice:
                case DeviceTypeVirtIO.FileSystemDevice:
                case DeviceTypeVirtIO.PMEMDevice:
                case DeviceTypeVirtIO.RPMBDevice:
                case DeviceTypeVirtIO.Mac80211HwsimWirelessSimulationDevice:
                case DeviceTypeVirtIO.VideoEncoderDevice:
                case DeviceTypeVirtIO.VideoDecoderDevice:
                case DeviceTypeVirtIO.SCMIDevice:
                case DeviceTypeVirtIO.NitroSecureModule:
                case DeviceTypeVirtIO.I2C_adapter:
                case DeviceTypeVirtIO.Watchdog:
                case DeviceTypeVirtIO.CAN_device:
                case DeviceTypeVirtIO.ParameterServer:
                case DeviceTypeVirtIO.AudioPolicyDevice:
                case DeviceTypeVirtIO.BluetoothDevice:
                case DeviceTypeVirtIO.GPIO_device:
                case DeviceTypeVirtIO.RDMA_device:
                    output.Add(item);
                    break;
            }
        }

        _devices_filtered = output.ToArray();
        return _devices_filtered;
    }

    public static BaseVirtIODevice[] GetDevices()
    {
        if (_devices != null)
        {
            return _devices;
        }

        var output = new List<BaseVirtIODevice>();
        foreach (var device in PCI.Devices)
        {
            if (device.VendorID == 0x1AF4)
            {
                try
                {
                    var d = new BaseVirtIODevice(device); // = new BaseVirtIODevice(device);
                    switch ((DeviceTypeVirtIO)device.DeviceID)
                    {
                        case DeviceTypeVirtIO.EntropySource:
                            d = new EntropyVirtIO(device);
                            break;
                        case DeviceTypeVirtIO.GPU_device:

                            d = new GPUVirtIODevice(device);
                            break;
                        default:
                            d = new BaseVirtIODevice(device);
                            break;
                    }

                    Console.WriteLine(device.DeviceID);
                    Console.WriteLine(d != null);
                    output.Add(d);
                    device.Claimed = true;

                    // d.Initialization();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    device.Claimed = false;
                }
            }
        }

        _devices = output.ToArray();
        return _devices;
    }
}
