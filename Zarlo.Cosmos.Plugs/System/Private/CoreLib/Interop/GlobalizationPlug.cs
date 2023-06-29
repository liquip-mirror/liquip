using IL2CPU.API.Attribs;

namespace Zarlo.Cosmos.Plugs.System.Private.CoreLib.Interop;

[Plug("Interop+Globalization, System.Private.CoreLib")]
public class GlobalizationPlug
{
    public static unsafe int ToAscii(uint flags, char* src, int srcLen, char* dstBuffer, int dstBufferCapacity)
    {
        var dstIndex = 0;
        for (var i = 0; i < srcLen; i++)
        {
            var c = src[i];
            if (c < 255)
            {
                if (dstIndex > dstBufferCapacity - 1)
                {
                    return 0; // error need a bigger buffer
                }

                dstBuffer[dstIndex] = c;
                dstIndex++;
            }
        }

        dstBuffer[dstIndex] = (char)0x00;
        return dstIndex;
    }

    public static unsafe int ToUnicode(uint flags, char* src, int srcLen, char* dstBuffer, int dstBufferCapacity)
    {
        var dstIndex = 0;
        for (var i = 0; i < srcLen; i++)
        {
            var c = src[i];

            if (dstIndex > dstBufferCapacity - 1)
            {
                return 0; // error need a bigger buffer
            }

            dstBuffer[dstIndex] = c;
            dstIndex++;
        }

        dstBuffer[dstIndex] = (char)0x00;
        return dstIndex;
    }
}
