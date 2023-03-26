// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Network;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.Zarlo.Net.Web;
using Sys = Cosmos.System;

namespace Test
{
    public class Kernel: Sys.Kernel
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
            //
            // Console.WriteLine();
            // Console.Write("rdSeed64:");
            // Console.WriteLine(rdSeed64 != rdSeed64_0);
            // Console.Write("rdSeed32:");
            // Console.WriteLine(rdSeed32 != rdSeed32_0);
            // // Console.WriteLine(rdSeed16 != rdSeed16_0);
            //
            // var powerManagementInformation = Cosmos.Zarlo.CPUID.PowerManagementInformation;
            //
            // Console.WriteLine("PowerManagementInformation");
            // Console.WriteLine(powerManagementInformation.DebugString());
            //
            // var cacheConfiguration = Cosmos.Zarlo.CPUID.CacheConfiguration;
            //
            // Console.WriteLine("CacheConfiguration");
            // Console.WriteLine(cacheConfiguration.DebugString());
            //
            // var processorFrequencyInformation = Cosmos.Zarlo.CPUID.ProcessorFrequencyInformation;
            //
            // Console.WriteLine("ProcessorFrequencyInformation");
            // Console.WriteLine(processorFrequencyInformation.DebugString());

            new Uri("http://google.com");
            
            // var wc = new HttpClient();
            // wc.Get(new Uri("http://google.com"));
            
            Console.WriteLine("DONE");
            while (true)
            {
            }
            
        }
    }
}
