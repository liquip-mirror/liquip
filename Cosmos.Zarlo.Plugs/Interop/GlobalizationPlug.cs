using IL2CPU.API.Attribs;

namespace Cosmos.Zarlo.Plugs.Interop;

[Plug("Interop+Globalization, System.Private.CoreLib")]
public class GlobalizationPlug
{
    // [PlugMethod(Signature = 
    //     "System_Int32__Interop_Globalization_ToAscii_System_UInt32__System_Char#__System_Int32__System_Char#__System_Int32_"
    // )]
    public static unsafe int ToAscii(uint flags, char* src, int srcLen, char* dstBuffer, int dstBufferCapacity)
    {
        var dstIndex = 0;
        for (var i = 0; i < srcLen; i++)
        {
            var c = src[i];
            if (c < 255)
            {
                if (dstIndex > dstBufferCapacity - 1) return 0; // error need a bigger buffer
                dstBuffer[dstIndex] = c;
                dstIndex++;
            }
        }

        dstBuffer[dstIndex] = (Char)0x00;
        return dstIndex;
    }

    // [PlugMethod(Signature = 
    //         "System_Int32__Interop_Globalization_ToUnicode_System_UInt32__System_Char#__System_Int32__System_Char#__System_Int32_"
    // )]
    public static unsafe int ToUnicode(uint flags, char* src, int srcLen, char* dstBuffer, int dstBufferCapacity)
    {
        var dstIndex = 0;
        for (var i = 0; i < srcLen; i++)
        {
            var c = src[i];

            if (dstIndex > dstBufferCapacity - 1) return 0; // error need a bigger buffer
            dstBuffer[dstIndex] = c;
            dstIndex++;
        }

        dstBuffer[dstIndex] = (Char)0x00;
        return dstIndex;
    }
}