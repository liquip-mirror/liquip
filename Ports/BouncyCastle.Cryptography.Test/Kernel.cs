using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Sys = Cosmos.System;


namespace BouncyCastle.Test;

public class Kernel : Sys.Kernel
{
    protected override void Run()
    {
        var random = new SecureRandom();
        var iv = random.GenerateSeed(10);


    }
}
