// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Cosmos.Core_Plugs.System;
using Cosmos.HAL;
using Cosmos.System.Network;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.Zarlo.Driver.VirtIO;
using Cosmos.Zarlo.ELF;
using Cosmos.Zarlo.Threading.Tasks;
using Org.BouncyCastle.Security;
using Sys = Cosmos.System;

namespace Test;

public class Kernel : Sys.Kernel
{
    protected override void BeforeRun()
    {
        Console.Clear();
        Console.WriteLine("Cosmos.Zarlo Test");
    }

    protected override void Run()
    {

        // Console.Write("RDseed.IsSupported: ");
        // Console.WriteLine(Cosmos.Zarlo.RDseed.IsSupported());
        // var rdSeed64 = Cosmos.Zarlo.RDseed.GetRDSeed64();
        // var rdSeed32 = Cosmos.Zarlo.RDseed.GetRDSeed32();
        // Console.Write("rdSeed64:");
        // Console.WriteLine((ulong)rdSeed64);
        // Console.Write("rdSeed32:");
        // Console.WriteLine((uint)rdSeed32);
        // Console.WriteLine();
        // var rdSeed64_0 = Cosmos.Zarlo.RDseed.GetRDSeed64();
        // var rdSeed32_0 = Cosmos.Zarlo.RDseed.GetRDSeed32();
        // Console.Write("rdSeed64:");
        // Console.WriteLine((ulong)rdSeed64_0);
        // Console.Write("rdSeed32:");
        // Console.WriteLine((uint)rdSeed32_0);

        // Console.WriteLine();
        // Console.Write("rdSeed64:");
        // Console.WriteLine(rdSeed64 != rdSeed64_0);
        // Console.Write("rdSeed32:");
        // Console.WriteLine(rdSeed32 != rdSeed32_0);

        // GC.Collect();

        // var powerManagementInformation = Cosmos.Zarlo.CPUID.PowerManagementInformation;
        // Console.WriteLine("PowerManagementInformation");
        // Console.WriteLine(powerManagementInformation.DebugString());

        // GC.Collect();

        // var cacheConfiguration = Cosmos.Zarlo.CPUID.CacheConfiguration;
        // Console.WriteLine("CacheConfiguration");
        // Console.WriteLine(cacheConfiguration.DebugString());

        // GC.Collect();

        // var processorFrequencyInformation = Cosmos.Zarlo.CPUID.ProcessorFrequencyInformation;
        // Console.WriteLine("ProcessorFrequencyInformation");
        // Console.WriteLine(processorFrequencyInformation.DebugString());

        // GC.Collect();

        // var featureFlags = Cosmos.Zarlo.CPUID.FeatureFlags;
        // Console.WriteLine("FeatureFlags");
        // Console.Write("eax0: ");
        // Console.Write(featureFlags.eax);
        // Console.Write(" ebx0: ");
        // Console.Write(featureFlags.ebx);
        // Console.Write(" ecx0: ");
        // Console.Write(featureFlags.ecx);
        // Console.Write(" edx0: ");
        // Console.Write(featureFlags.edx);

        Console.WriteLine("loading VirtIO Drivers");
        Console.WriteLine("VID DID RID SC BAR0 Type");
        foreach (BaseVirtIODevice device in VirtIOManager.GetDevices())
        {
            var deviceType = device.DeviceType;
            
            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}",
                IntToString(device.VendorID),
                IntToString(device.DeviceID),
                IntToString(device.RevisionID),
                IntToString(device.Subclass),
                IntToString((int)device.BAR0),
                deviceType.AsString(),
                Convert.ToString(device.GetDeviceFeaturesFlag(), 2)
            );


            Console.WriteLine();

        }

        Console.WriteLine("DONE");
        while (true)
        {

        }
    }


    public static string IntToString(int value)
    {
        char[] baseChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         'A', 'B', 'C', 'D', 'E', 'F'};
        string result = string.Empty;
        int targetBase = baseChars.Length;

        do
        {
            result = baseChars[value % targetBase] + result;
            value = value / targetBase;
        }
        while (value > 0);

        return result;
    }

}

