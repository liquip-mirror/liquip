using IL2CPU.API.Attribs;

namespace Liquip.Plugs.System.Private.CoreLib.Interop.Sys;

[Plug("Interop, System.Private.CoreLib")]
public class GetRandomBytesPlug
{
    private static readonly Random Random = new Random();
    public static unsafe void GetRandomBytes(byte* buffer, int length)
    {
        var b = new byte[length];
        Random.NextBytes(b);
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
