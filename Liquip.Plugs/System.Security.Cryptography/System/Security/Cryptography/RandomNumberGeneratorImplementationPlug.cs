using IL2CPU.API.Attribs;
using Liquip.Plugs.System.Private.CoreLib.Interop.Sys;

using B = System.Security.Cryptography.RandomNumberGenerator;

namespace Liquip.Plugs.System.Security.Cryptography.System.Security.Cryptography;

[Plug("System.Security.Cryptography.RandomNumberGeneratorImplementation, System.Security.Cryptography.Algorithms")]
// [Plug(Target = typeof(B))]
public static class RandomNumberGeneratorImplementationPlug
{
    [PlugMethod(Signature = "System_Void__System_Security_Cryptography_RandomNumberGeneratorImplementation_GetBytes_System_Byte#__System_Int32_")]
    public static unsafe void GetBytes(byte* pbBuffer, int count)
    {
        GetRandomBytesPlug.GetCryptographicallySecureRandomBytes(pbBuffer, count);
    }
}
