using Org.BouncyCastle.Security;
using Sys = Cosmos.System;


namespace BouncyCastle.Cryptography.Test;

public class Kernel : Sys.Kernel
{
    protected override void Run()
    {
        var random = new SecureRandom();
        var iv = random.GenerateSeed(10);


    }
}
