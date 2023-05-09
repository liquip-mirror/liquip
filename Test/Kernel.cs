// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Cosmos.Core_Plugs.System;
using Cosmos.HAL;
using Cosmos.System.Network;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.Zarlo;
using Cosmos.Zarlo.Driver.VirtIO;
using Cosmos.Zarlo.ELF;
using Cosmos.Zarlo.Threading.Tasks;
using Org.BouncyCastle.Security;
using Sys = Cosmos.System;

namespace Test;

public class Kernel : Sys.Kernel
{

    protected virtual void OnBoot()
    {
        base.OnBoot();
        Cosmos.Zarlo.Threading.Core.Processing.ProcessorScheduler.Initialize();
        var i = false;
        if (i)
        {
            Cosmos.Zarlo.Threading.Core.Processing.ProcessorScheduler.SwitchTask();
            Cosmos.Zarlo.Threading.Core.Processing.ProcessorScheduler.EntryPoint();
        }
    }

    protected override void BeforeRun()
    {
        Console.Clear();
        Console.WriteLine("Cosmos.Zarlo Test");
    }

    protected override void Run()
    {


        // Console.WriteLine(CPUID.FeatureFlags.DebugString());

        Console.WriteLine(CPUID.PowerManagementInformation.DebugString());

        Console.WriteLine(CPUID.CacheConfiguration.DebugString());


        Console.WriteLine("DONE");

        while (true) { }

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

