// using IL2CPU.API.Attribs;
// using Liquip.Plugs.System.Private.CoreLib.Interop.Sys;
//
// namespace Liquip.Plugs.System.Security.Cryptography.Algorithms.Interop;
//
// [Plug("Interop+Crypto, System.Security.Cryptography.Algorithms")]
// public class CryptoPlug
// {
//
//     [PlugMethod(Signature = "System_Boolean__Interop_Crypto_CryptoNative_GetRandomBytes_System_Byte#__System_Int32_")]
//     public static unsafe bool GetBytes(byte* pbBuffer, int count)
//     {
//         GetRandomBytesPlug.GetCryptographicallySecureRandomBytes(pbBuffer, count);
//         return true;
//     }
//
//     [PlugMethod(Signature = "System_UInt64__Interop_Crypto_ErrGetErrorAlloc__System_Boolean_")]
//     public static unsafe ulong ErrGetErrorAlloc(bool a)
//     {
//         return 1;
//     }
//
// }
//
