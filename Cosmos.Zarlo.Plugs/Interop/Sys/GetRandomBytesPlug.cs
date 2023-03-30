using IL2CPU.API.Attribs;

namespace Cosmos.Zarlo.Plugs.Interop.Sys;

[Plug("Interop, System.Private.CoreLib")]
public class GetRandomBytesPlug
{
    public static unsafe void GetRandomBytes(byte* buffer, int length)
    {
        var random = new Random();
        var b = new byte[length];
        random.NextBytes(b);
        for (var i = 0; i < length; i++)
        {
            buffer[i] = b[i];
        }
    }

    public static unsafe void GetCryptographicallySecureRandomBytes(byte* buffer, int length)
    {
        var random = new Random(RDseed.GetRDSeed32());
        var b = new byte[length];
        random.NextBytes(b);
        for (var i = 0; i < length; i++)
        {
            buffer[i] = b[i];
        }
    }
}