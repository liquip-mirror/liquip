using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace Test
{
    public class Kernel: Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
        }
        
        protected override void Run()
        {
            var rdSeed64 = Cosmos.Zarlo.RDseed.GetRDSeed64();
            var rdSeed32 = Cosmos.Zarlo.RDseed.GetRDSeed32();
            var rdSeed16 = Cosmos.Zarlo.RDseed.GetRDSeed16();
            Console.WriteLine(rdSeed64);
            Console.WriteLine(rdSeed32);
            Console.WriteLine(rdSeed16);

            var rdSeed64_0 = Cosmos.Zarlo.RDseed.GetRDSeed64();
            var rdSeed32_0 = Cosmos.Zarlo.RDseed.GetRDSeed32();
            var rdSeed16_0 = Cosmos.Zarlo.RDseed.GetRDSeed16();
            Console.WriteLine(rdSeed64_0);
            Console.WriteLine(rdSeed32_0);
            Console.WriteLine(rdSeed16_0);
            
            Console.WriteLine(rdSeed64 != rdSeed64_0);
            Console.WriteLine(rdSeed32 != rdSeed32_0);
            Console.WriteLine(rdSeed16 != rdSeed16_0);
            
        }
    }
}
